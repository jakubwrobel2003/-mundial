import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, merge, EMPTY } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Team } from '../models/team.model';
import { MatchPrediction } from '../models/prediction.model';

export interface RecentMatch {
  date: string;
  opponent: string;
  result: 'W' | 'D' | 'L';
  goalsFor: number;
  goalsAgainst: number;
  competition: string;
  isHome: boolean;
}

export interface TeamRecentForm {
  teamName: string;
  played: number;
  won: number;
  drawn: number;
  lost: number;
  goalsScored: number;
  goalsConceded: number;
  matches: RecentMatch[];
}

export interface ClubPlayerStats {
  playerName: string;
  club: string;
  competition: string;
  goals: number;
  assists: number;
  penalties: number;
}

export interface EnrichedPrediction {
  basePrediction: MatchPrediction;
  claudeAnalysis: string | null;
  footballDataContext: string | null;
  homeRecentForm: string | null;
  awayRecentForm: string | null;
  homeFormData: TeamRecentForm | null;
  awayFormData: TeamRecentForm | null;
  homeFormStats: string | null;
  awayFormStats: string | null;
  homePlayerClubStats: ClubPlayerStats[];
  awayPlayerClubStats: ClubPlayerStats[];
  tavilyHomeNews: string | null;
  tavilyAwayNews: string | null;
  tavilyH2H: string | null;
  tavilyHomeQualifiers: string | null;
  tavilyAwayQualifiers: string | null;
  ragDocumentsUsed: number;
  dataSourcesUsed: string[];
}

export interface WcTeamRef {
  id: string | null;
  fdName: string;
  name: string;
  flagEmoji: string;
  known: boolean;
}

export interface WcMatch {
  matchday: number;
  stage: string;
  fdStage: string;
  group: string | null;
  utcDate: string | null;
  status: 'TIMED' | 'SCHEDULED' | 'IN_PLAY' | 'PAUSED' | 'FINISHED' | 'SUSPENDED' | 'CANCELLED';
  homeTeam: WcTeamRef;
  awayTeam: WcTeamRef;
  scoreHome: number | null;
  scoreAway: number | null;
  venue: string | null;
}

export interface AnalysisHistoryItem {
  id: number;
  homeTeamId: string;
  awayTeamId: string;
  stage: string;
  homeWinProbability: number;
  drawProbability: number;
  awayWinProbability: number;
  homeExpectedGoals: number;
  awayExpectedGoals: number;
  predictedHomeGoals: number;
  predictedAwayGoals: number;
  claudeAnalysis: string | null;
  usedFootballData: boolean;
  usedTavily: boolean;
  usedRag: boolean;
  predictionJson: string;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  // Lokalnie: Angular dev server (:4200) → backend (:5000). Produkcja: same-origin /api
  private readonly base = window.location.hostname === 'localhost'
    ? 'http://localhost:5000/api'
    : '/api';

  // Cache wyników Poisson – wynik jest deterministyczny (statyczna baza drużyn),
  // więc ta sama para/etap zawsze daje ten sam wynik. Ważne przez całą sesję.
  private readonly _predictionCache = new Map<string, MatchPrediction>();

  constructor(private http: HttpClient) {}

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`${this.base}/teams`);
  }

  getTeam(id: string): Observable<Team> {
    return this.http.get<Team>(`${this.base}/teams/${id}`);
  }

  analyzePrediction(homeTeamId: string, awayTeamId: string, stage = 'Group'): Observable<MatchPrediction> {
    const key = `${homeTeamId}:${awayTeamId}:${stage}`;
    const cached = this._predictionCache.get(key);
    if (cached) return of(cached);

    const params = new HttpParams()
      .set('homeTeamId', homeTeamId)
      .set('awayTeamId', awayTeamId)
      .set('stage', stage);
    return this.http.get<MatchPrediction>(`${this.base}/predictions/analyze`, { params }).pipe(
      tap(p => this._predictionCache.set(key, p))
    );
  }

  getWc2026Schedule(): Observable<WcMatch[]> {
    const LS_KEY = 'wc2026_schedule_v1';
    const TTL_MS = 24 * 60 * 60 * 1000; // 24h

    let staleData: WcMatch[] | null = null;
    try {
      const raw = localStorage.getItem(LS_KEY);
      if (raw) {
        const { data, ts } = JSON.parse(raw) as { data: WcMatch[]; ts: number };
        if (Date.now() - ts < TTL_MS) return of(data); // świeże – zwróć natychmiast
        staleData = data; // przeterminowane – pokaż od razu, odśwież w tle
      }
    } catch { /* localStorage niedostępny – przejdź do HTTP */ }

    const fresh$ = this.http.get<WcMatch[]>(`${this.base}/matches/wc2026`).pipe(
      tap(data => {
        try { localStorage.setItem(LS_KEY, JSON.stringify({ data, ts: Date.now() })); }
        catch { /* quota exceeded – ignoruj */ }
      })
    );

    // Stale-while-revalidate: pokaż stare dane natychmiast, cicho zastąp świeżymi
    if (staleData) return merge(of(staleData), fresh$.pipe(catchError(() => EMPTY)));
    return fresh$;
  }

  getAnalysisHistory(limit = 20): Observable<AnalysisHistoryItem[]> {
    return this.http.get<AnalysisHistoryItem[]>(`${this.base}/enrichedpredictions/history?limit=${limit}`);
  }

  // Cache pełnej analizy AI – TTL 30 min (dane Tavily mogą się zmienić)
  private readonly _enrichedCache = new Map<string, { data: EnrichedPrediction; ts: number }>();
  private static readonly ENRICHED_TTL = 30 * 60 * 1000;

  analyzeEnriched(
    homeTeamId: string,
    awayTeamId: string,
    stage = 'Group',
    useWeb = true,
    useFootballData = true,
  ): Observable<EnrichedPrediction> {
    const key = `${homeTeamId}:${awayTeamId}:${stage}:${useWeb}:${useFootballData}`;
    const cached = this._enrichedCache.get(key);
    if (cached && Date.now() - cached.ts < ApiService.ENRICHED_TTL) return of(cached.data);

    const params = new HttpParams()
      .set('stage', stage)
      .set('useWeb', useWeb)
      .set('useFootballData', useFootballData);
    return this.http.get<EnrichedPrediction>(
      `${this.base}/enrichedpredictions/${homeTeamId}/${awayTeamId}`,
      { params },
    ).pipe(
      tap(data => this._enrichedCache.set(key, { data, ts: Date.now() }))
    );
  }
}
