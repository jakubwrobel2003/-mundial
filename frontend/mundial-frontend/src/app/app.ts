import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { ApiService, EnrichedPrediction, TeamRecentForm, ClubPlayerStats, WcMatch } from './services/api.service';
import { Team } from './models/team.model';
import { MatchPrediction, PlayerPrediction } from './models/prediction.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  teams: Team[] = [];
  homeTeamId = '';
  awayTeamId = '';
  stage = 'Group';
  stages = ['Group', 'Round of 32', 'Round of 16', 'Quarter-Final', 'Semi-Final', 'Final'];

  prediction: MatchPrediction | null = null;
  enriched: EnrichedPrediction | null = null;
  loading = false;
  enrichedLoading = false;
  error = '';
  activeTab = 'overview';
  useWeb = true;
  useFootballData = true;

  // Terminarz WC 2026
  selectorMode: 'schedule' | 'manual' = 'schedule';
  schedule: WcMatch[] = [];
  scheduleLoading = false;
  scheduleError = '';
  scheduleFilter: 'all' | 'upcoming' | 'finished' = 'all';
  scheduleStageFilter = 'all';

  constructor(
    private api: ApiService,
    private sanitizer: DomSanitizer,
  ) {}

  ngOnInit() {
    this.api.getTeams().subscribe({
      next: (teams) => (this.teams = teams.sort((a, b) => a.fifaRanking - b.fifaRanking)),
      error: () => (this.error = 'Nie można załadować drużyn. Upewnij się że backend działa na porcie 5000.'),
    });
    this.loadSchedule();
  }

  loadSchedule() {
    this.scheduleLoading = true;
    this.scheduleError = '';
    this.api.getWc2026Schedule().subscribe({
      next: (matches) => {
        this.schedule = matches;
        this.scheduleLoading = false;
      },
      error: () => {
        this.scheduleError = 'Nie można pobrać terminarza – brak klucza FOOTBALL_DATA_API_KEY lub serwer offline.';
        this.scheduleLoading = false;
        this.selectorMode = 'manual';
      },
    });
  }

  selectMatch(m: WcMatch) {
    if (!m.homeTeam.known || !m.awayTeam.known) return;
    this.homeTeamId = m.homeTeam.id!;
    this.awayTeamId = m.awayTeam.id!;
    this.stage = m.stage;
    // Reset wyników
    this.prediction = null;
    this.enriched = null;
    this.error = '';
  }

  get scheduleStages(): string[] {
    const seen = new Set<string>();
    for (const m of this.schedule) if (m.stage) seen.add(m.stage);
    return ['all', ...seen];
  }

  get filteredSchedule(): WcMatch[] {
    return this.schedule.filter(m => {
      if (this.scheduleFilter === 'upcoming' && m.status === 'FINISHED') return false;
      if (this.scheduleFilter === 'finished' && m.status !== 'FINISHED') return false;
      if (this.scheduleStageFilter !== 'all' && m.stage !== this.scheduleStageFilter) return false;
      return true;
    });
  }

  // Grupuje przefiltrowane mecze wg etapu → grupy → matchday
  get groupedSchedule(): { stage: string; group: string | null; matchday: number; matches: WcMatch[] }[] {
    const map = new Map<string, WcMatch[]>();
    for (const m of this.filteredSchedule) {
      const key = `${m.stage}||${m.group ?? ''}||${m.matchday}`;
      if (!map.has(key)) map.set(key, []);
      map.get(key)!.push(m);
    }
    return [...map.entries()].map(([key, matches]) => {
      const [stage, group, matchday] = key.split('||');
      return { stage, group: group || null, matchday: Number(matchday), matches };
    });
  }

  statusLabel(status: string): string {
    return ({ TIMED: '📅 Zaplanowany', SCHEDULED: '📅 Zaplanowany', IN_PLAY: '🔴 Na żywo', PAUSED: '⏸ Przerwa', FINISHED: '✅ Zakończony', SUSPENDED: '⚠️ Wstrzymany', CANCELLED: '❌ Odwołany' } as Record<string, string>)[status] ?? status;
  }

  statusClass(status: string): string {
    return ({ FINISHED: 'status-done', IN_PLAY: 'status-live', PAUSED: 'status-live' } as Record<string, string>)[status] ?? 'status-upcoming';
  }

  formatMatchDate(utcDate: string | null): string {
    if (!utcDate) return '–';
    const d = new Date(utcDate);
    return d.toLocaleString('pl-PL', { day: '2-digit', month: '2-digit', hour: '2-digit', minute: '2-digit' });
  }

  analyze() {
    if (!this.homeTeamId || !this.awayTeamId || this.homeTeamId === this.awayTeamId) return;
    this.loading = true;
    this.error = '';
    this.prediction = null;
    this.enriched = null;
    this.activeTab = 'overview';

    this.api.analyzePrediction(this.homeTeamId, this.awayTeamId, this.stage).subscribe({
      next: (p) => {
        this.prediction = p;
        this.loading = false;
      },
      error: () => {
        this.error = 'Analiza nie powiodła się. Sprawdź połączenie z backendem.';
        this.loading = false;
      },
    });
  }

  analyzeWithClaude() {
    if (!this.homeTeamId || !this.awayTeamId || this.homeTeamId === this.awayTeamId) return;
    this.enrichedLoading = true;
    this.error = '';
    this.enriched = null;
    this.prediction = null;

    this.api
      .analyzeEnriched(this.homeTeamId, this.awayTeamId, this.stage, this.useWeb, this.useFootballData)
      .subscribe({
        next: (result) => {
          this.enriched = result;
          this.prediction = result.basePrediction;
          this.enrichedLoading = false;
          this.activeTab = 'claude';
        },
        error: () => {
          this.error = 'Pełna analiza nie powiodła się. Sprawdź klucze API w .env.';
          this.enrichedLoading = false;
        },
      });
  }

  swapTeams() {
    [this.homeTeamId, this.awayTeamId] = [this.awayTeamId, this.homeTeamId];
    if (this.prediction) this.analyze();
  }

  teamById(id: string): Team | undefined {
    return this.teams.find((t) => t.id === id);
  }

  topPlayers(players: PlayerPrediction[]): PlayerPrediction[] {
    return players.slice(0, 7);
  }

  threatColor(level: string): string {
    return (
      ({ Elite: '#ef4444', High: '#f97316', Medium: '#eab308', Low: '#22c55e' } as Record<string, string>)[level] ??
      '#6b7280'
    );
  }

  resultColor(r: string): string {
    return r === 'W' ? '#22c55e' : r === 'D' ? '#eab308' : '#ef4444';
  }

  formDots(results: string): { r: string }[] {
    return results.split(' ').map((r) => ({ r }));
  }

  confidenceColor(c: number): string {
    if (c >= 85) return '#22c55e';
    if (c >= 70) return '#eab308';
    return '#f97316';
  }

  /** Renderuje tekst analizy Claude jako HTML z obsługą markdown */
  renderMarkdown(text: string): SafeHtml {
    if (!text) return '';
    let html = '';
    const lines = text.split('\n');
    let inTable = false;
    let tableHeader = false;

    for (let i = 0; i < lines.length; i++) {
      const line = lines[i];
      const trimmed = line.trim();

      // Nagłówki
      if (trimmed.startsWith('### ')) {
        if (inTable) { html += '</table>'; inTable = false; }
        html += `<h4>${this.inlineMarkdown(trimmed.slice(4))}</h4>`;
        continue;
      }
      if (trimmed.startsWith('## ')) {
        if (inTable) { html += '</table>'; inTable = false; }
        html += `<h3>${this.inlineMarkdown(trimmed.slice(3))}</h3>`;
        continue;
      }
      if (trimmed.startsWith('# ')) {
        if (inTable) { html += '</table>'; inTable = false; }
        html += `<h2>${this.inlineMarkdown(trimmed.slice(2))}</h2>`;
        continue;
      }

      // Separator tabeli
      if (/^\|[-| ]+\|$/.test(trimmed)) {
        tableHeader = false;
        continue;
      }

      // Wiersz tabeli
      if (trimmed.startsWith('|') && trimmed.endsWith('|')) {
        if (!inTable) {
          html += '<table class="md-table"><thead>';
          inTable = true;
          tableHeader = true;
        }
        const cells = trimmed.slice(1, -1).split('|').map((c) => c.trim());
        const tag = tableHeader ? 'th' : 'td';
        if (!tableHeader && lines[i - 1]?.trim().startsWith('|---')) {
          html += '</thead><tbody>';
        }
        html += `<tr>${cells.map((c) => `<${tag}>${this.inlineMarkdown(c)}</${tag}>`).join('')}</tr>`;
        continue;
      }

      if (inTable) {
        html += '</tbody></table>';
        inTable = false;
      }

      // Separator
      if (trimmed === '---') {
        html += '<hr>';
        continue;
      }

      // Pusta linia
      if (!trimmed) {
        html += '<br>';
        continue;
      }

      // Punkt listy
      if (trimmed.startsWith('- ') || trimmed.startsWith('• ')) {
        html += `<li>${this.inlineMarkdown(trimmed.slice(2))}</li>`;
        continue;
      }

      // Paragraf
      html += `<p>${this.inlineMarkdown(trimmed)}</p>`;
    }

    if (inTable) html += '</tbody></table>';
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }

  private inlineMarkdown(text: string): string {
    return text
      .replace(/\*\*(.+?)\*\*/g, '<strong>$1</strong>')
      .replace(/\*(.+?)\*/g, '<em>$1</em>')
      .replace(/`(.+?)`/g, '<code>$1</code>');
  }

  /** Zwraca procentowy udział goli (dla paska) */
  goalShare(home: number, away: number): number {
    const total = home + away;
    return total > 0 ? Math.round((home / total) * 100) : 50;
  }

  winBarStyle(prob: number, color: string): string {
    return `width:${prob}%; background:${color}`;
  }

  /** Pasek słupkowy formy (procent wygranych) */
  formWinPct(form: TeamRecentForm): number {
    return form.played > 0 ? Math.round((form.won / form.played) * 100) : 0;
  }

  /** Szuka statystyk klubowych dla zawodnika po nazwisku */
  findClubStats(playerName: string, stats: ClubPlayerStats[]): ClubPlayerStats | null {
    const lastName = playerName.split(' ').pop()?.toLowerCase() ?? '';
    return (
      stats.find(
        (s) =>
          s.playerName.toLowerCase().includes(lastName) || playerName.toLowerCase().includes(s.playerName.split(' ').pop()?.toLowerCase() ?? ''),
      ) ?? null
    );
  }
}
