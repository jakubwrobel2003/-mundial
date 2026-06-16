import { Team } from './team.model';

export interface ScorePrediction {
  homeGoals: number;
  awayGoals: number;
  probability: number;
  label: string;
}

export interface PlayerPrediction {
  playerId: string;
  playerName: string;
  club: string;
  position: string;
  isStar: boolean;
  expectedGoals: number;
  expectedAssists: number;
  expectedShots: number;
  expectedShotsOnTarget: number;
  expectedFoulsCommitted: number;
  expectedFoulsDrawn: number;
  yellowCardProbability: number;
  redCardProbability: number;
  expectedKeyPasses: number;
  expectedDribbles: number;
  shotsOverUnderLine: number;
  shotsOverProbability: number;
  foulsOverUnderLine: number;
  foulsOverProbability: number;
  keyInsight: string;
  threatLevel: string;
}

export interface BettingAngles {
  over15GoalsProbability: number;
  over25GoalsProbability: number;
  over35GoalsProbability: number;
  bothTeamsToScoreProbability: number;
  homeCleanSheetProbability: number;
  awayCleanSheetProbability: number;
  over35CardsProbability: number;
  over45CardsProbability: number;
  over55CardsProbability: number;
  over85FoulsProbability: number;
  over105FoulsProbability: number;
  moreFoulsTeam: string;
  moreFoulsProbability: number;
  moreCardsTeam: string;
  moreCardsProbability: number;
  firstGoalTeam: string;
  firstGoalProbability: number;
  expectedCorners: number;
  over9CornersProbability: number;
  halfTimeResult: string;
  halfTimeResultProbability: number;
  valueBets: string[];
  avoidBets: string[];
}

export interface MatchPrediction {
  matchId: string;
  homeTeam: Team;
  awayTeam: Team;
  homeWinProbability: number;
  drawProbability: number;
  awayWinProbability: number;
  homeExpectedGoals: number;
  awayExpectedGoals: number;
  predictedHomeGoals: number;
  predictedAwayGoals: number;
  predictedScoreProbability: number;
  topScores: ScorePrediction[];
  expectedTotalCards: number;
  expectedTotalFouls: number;
  expectedTotalShots: number;
  expectedCorners: number;
  matchSummary: string;
  keyFactors: string[];
  tacticalInsights: string[];
  dangerZones: string[];
  analysisConfidence: number;
  verdict: string;
  homePlayerPredictions: PlayerPrediction[];
  awayPlayerPredictions: PlayerPrediction[];
  bettingAngles: BettingAngles;
  generatedAt: string;
}
