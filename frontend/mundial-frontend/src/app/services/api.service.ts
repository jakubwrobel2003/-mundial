import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
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

@Injectable({ providedIn: 'root' })
export class ApiService {
  // Lokalnie: Angular dev server (:4200) → backend (:5000). Produkcja: same-origin /api
  private readonly base = window.location.hostname === 'localhost'
    ? 'http://localhost:5000/api'
    : '/api';

  constructor(private http: HttpClient) {}

  getTeams(): Observable<Team[]> {
    return this.http.get<Team[]>(`${this.base}/teams`);
  }

  getTeam(id: string): Observable<Team> {
    return this.http.get<Team>(`${this.base}/teams/${id}`);
  }

  analyzePrediction(homeTeamId: string, awayTeamId: string, stage = 'Group'): Observable<MatchPrediction> {
    const params = new HttpParams()
      .set('homeTeamId', homeTeamId)
      .set('awayTeamId', awayTeamId)
      .set('stage', stage);
    return this.http.get<MatchPrediction>(`${this.base}/predictions/analyze`, { params });
  }

  getWc2026Schedule(): Observable<WcMatch[]> {
    return this.http.get<WcMatch[]>(`${this.base}/matches/wc2026`);
  }

  analyzeEnriched(
    homeTeamId: string,
    awayTeamId: string,
    stage = 'Group',
    useWeb = true,
    useFootballData = true,
  ): Observable<EnrichedPrediction> {
    const params = new HttpParams()
      .set('stage', stage)
      .set('useWeb', useWeb)
      .set('useFootballData', useFootballData);
    return this.http.get<EnrichedPrediction>(
      `${this.base}/enrichedpredictions/${homeTeamId}/${awayTeamId}`,
      { params },
    );
  }
}
