export interface Player {
  id: string;
  name: string;
  age: number;
  club: string;
  position: string;
  shirtNumber: number;
  isCaptain: boolean;
  isKeyStar: boolean;
  overallRating: number;
  currentForm: number;
  goalsPer90: number;
  assistsPer90: number;
  shotsPer90: number;
  shotsOnTargetPer90: number;
  keyPassesPer90: number;
  dribblesPer90: number;
  foulsCommittedPer90: number;
  foulsDrawnPer90: number;
  yellowCardsPer90: number;
  redCardsPer90: number;
  tacklesPer90: number;
  interceptionsPer90: number;
  aerialDuelsPer90: number;
  description: string;
  playingCharacteristics: string;
}

export interface Team {
  id: string;
  name: string;
  shortName: string;
  flagEmoji: string;
  confederation: string;
  group: string;
  fifaRanking: number;
  squadStrength: number;
  currentForm: number;
  goalsPerGame: number;
  goalsConcededPerGame: number;
  foulsPerGame: number;
  yellowCardsPerGame: number;
  redCardsPerGame?: number;
  possession: number;
  pressureIntensity: number;
  defensiveLineHeight?: number;
  shotsPerGame: number;
  shotsOnTargetPerGame?: number;
  primaryStyle: string;
  secondaryStyle?: string;
  managerName: string;
  tacticalSetup: string;
  description: string;
  recentResults: string;
  players?: Player[];
}
