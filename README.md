# Mundial 2026 – AI Prediction Engine

Aplikacja do predykcji meczów Mistrzostw Świata 2026. Łączy lokalny silnik Poisson xG z danymi na żywo z **football-data.org**, wyszukiwaniem **Tavily** i syntezą **Claude AI** – dając gotowe kupony zakładowe z uzasadnieniem.

---

## Stack

| Warstwa | Technologia |
|---|---|
| Backend | .NET 9 · ASP.NET Core · EF Core 9 |
| Baza danych | SQLite (EF Core + FTS5 dla RAG) |
| Frontend | Angular 21 · standalone components · SCSS |
| Silnik predykcji | Poisson xG (lokalny, bez API) |
| Dane live | football-data.org API v4 (free tier) |
| Wyszukiwanie | Tavily Search API |
| Analiza AI | Anthropic Claude (claude-sonnet-4-6) |

---

## Funkcje

- **Szybka analiza (Poisson)** – natychmiastowa, bez żadnych API
- **Pełna analiza AI** – pipeline 6 kroków: historia EC/WC → statystyki klubowe → Tavily news → RAG → Claude
- **Historia form** – mecze z Euro 2024 i MŚ 2026 (football-data.org, cache 7 dni)
- **Statystyki klubowe zawodników** – gole/asysty z Premier League, La Liga, Bundesligi, Serie A, Ligue 1 (cache 24h)
- **Eliminacje i towarzyskie** – pobierane przez Tavily i cache'owane 7 dni w SQLite RAG
- **RAG Knowledge Base** – SQLite FTS5, dokumenty z TTL
- **Rate limiting** – FixedWindowRateLimiter 10 req/min, kolejkuje zamiast odrzucać
- **6 zakładek UI**: Przegląd · Zawodnicy · Forma & Historia · Statystyki · Kupony · Claude AI

---

## Wymagania

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9)
- [Node.js 20+](https://nodejs.org/) + npm 10+
- [Angular CLI 21](https://angular.dev/): `npm install -g @angular/cli`
- Klucze API (patrz niżej)

---

## Konfiguracja

### 1. Klonowanie i struktura

```
Mundial/
├── backend/
│   ├── MundialPrediction.API/           ← :5000
│   ├── MundialPrediction.Core/          ← modele, interfejsy
│   └── MundialPrediction.Infrastructure/ ← EF, API clients, silnik
├── frontend/
│   └── mundial-frontend/                ← Angular :4200
├── .env                                 ← NIE commitować (w .gitignore)
├── .env.example                         ← szablon
└── Mundial.sln
```

### 2. Plik .env

Skopiuj `.env.example` do `.env` w katalogu głównym i uzupełnij klucze:

```bash
cp .env.example .env
```

```env
# football-data.org – https://www.football-data.org/client/register
FOOTBALL_DATA_API_KEY=twoj_klucz

# Tavily – https://tavily.com
TAVILY_API_KEY=tvly-twoj_klucz

# Anthropic – https://console.anthropic.com
ANTHROPIC_API_KEY=sk-ant-twoj_klucz

# Opcjonalne (mają wartości domyślne)
FOOTBALL_DATA_RATE_LIMIT_PER_MINUTE=10
CLAUDE_MODEL=claude-sonnet-4-6
CLAUDE_MAX_TOKENS=2000
DATABASE_PATH=./mundial2026.db
```

> Aplikacja zadziała bez kluczy Tavily i Anthropic – wtedy dostępna jest tylko szybka analiza Poisson. football-data.org jest wymagane do historii form i statystyk klubowych.

### 3. Uruchomienie backendu

```bash
cd C:/repo/mundial
dotnet run --project backend/MundialPrediction.API
```

API dostępne pod `http://localhost:5000`  
Swagger UI: `http://localhost:5000/swagger`

### 4. Uruchomienie frontendu

```bash
cd frontend/mundial-frontend
npm install
ng serve
```

Frontend dostępny pod `http://localhost:4200`

---

## Pipeline – Pełna analiza AI

```
Użytkownik: Wybiera drużyny + etap → klika "Pełna analiza AI"
                                              ↓
GET /api/enrichedpredictions/{homeId}/{awayId}?stage=Group
                                              ↓
┌─────────────────────────────────────────────────────────────┐
│ KROK 1  Poisson xG  (lokalny, ~0ms)                         │
│   xG = GoalsPerGame × GoalsConcededPerGame × homeAdvantage  │
│   Rozkład Poissona → topScores[], BettingAngles             │
├─────────────────────────────────────────────────────────────┤
│ KROK 2  Historia EC/WC  (football-data.org, sekwencyjnie)   │
│   Cache L1: IMemoryCache → L2: SQLite 7d → L3: API          │
│   GET /v4/competitions/EC/matches?season=2024               │
│   GET /v4/competitions/WC/matches?season=2026               │
│   → TeamRecentForm: W/D/L, bramki, lista meczów             │
├─────────────────────────────────────────────────────────────┤
│ KROK 3  Statystyki klubowe  (football-data.org)             │
│   Cache L1: Dictionary (per liga) → L2: SQLite 24h → L3: API│
│   GET /v4/competitions/{liga}/scorers?season=2024&limit=50  │
│   Ligi: PL · BL1 · SA · PD · FL1 · DED · PPL               │
│   Fuzzy match: exact name → last name contains              │
│   → ClubPlayerStats: Goals, Assists, Penalties              │
├─────────────────────────────────────────────────────────────┤
│ KROK 4  Dane równoległe (Task.WhenAll)                      │
│   ├─ football-data.org: wynik meczu WC 2026                 │
│   ├─ Tavily: wiadomości gospodarz                           │
│   ├─ Tavily: wiadomości gość                                │
│   ├─ Tavily: head-to-head historia                          │
│   ├─ Tavily/RAG: eliminacje + towarzyskie gospodarz (7d)    │
│   └─ Tavily/RAG: eliminacje + towarzyskie gość (7d)         │
├─────────────────────────────────────────────────────────────┤
│ KROK 5  RAG  (SQLite FTS5)                                  │
│   Wyszukiwanie full-text po wcześniej zapisanych dokumentach │
│   TTL: wiadomości 6h · eliminacje 7d                        │
├─────────────────────────────────────────────────────────────┤
│ KROK 6  Claude AI  (Anthropic API)                          │
│   Prompt: historia + statystyki klubowe + Poisson + news    │
│   → Analiza PL: czynnik kluczowy · taktyka · zakłady · wyrok│
├─────────────────────────────────────────────────────────────┤
│ KROK 7  Zapis do SQLite                                     │
│   PredictionHistory · RagDocuments                          │
└─────────────────────────────────────────────────────────────┘
                                              ↓
                              EnrichedMatchPrediction JSON
                                              ↓
                              Angular – 6 zakładek UI
```

---

## Endpointy API

| Metoda | Endpoint | Opis |
|---|---|---|
| `GET` | `/api/teams` | Lista 20 drużyn MŚ 2026 |
| `GET` | `/api/teams/{id}` | Dane drużyny z zawodnikami |
| `GET` | `/api/predictions/analyze` | Szybka analiza Poisson |
| `GET` | `/api/enrichedpredictions/{homeId}/{awayId}` | Pełna analiza AI |

**Parametry pełnej analizy:**

```
?stage=Group          # Group / Round of 16 / Quarter-Final / Semi-Final / Final
&useWeb=true          # Tavily news i H2H
&useFootballData=true # Historia EC/WC i statystyki klubowe
```

---

## Rate limiting

football-data.org free tier pozwala na **10 żądań/minutę**. Aplikacja używa `System.Threading.RateLimiting.FixedWindowRateLimiter` z `QueueLimit=50` – żądania przekraczające limit czekają na reset okna zamiast zwracać błąd.

Cache eliminuje powtarzające się wywołania:

| Dane | Cache L1 | Cache L2 (SQLite) |
|---|---|---|
| EC 2024 mecze | IMemoryCache | 7 dni |
| WC 2026 mecze | IMemoryCache | 2 min |
| Scorers liga | Dictionary (per proces) | 24h |
| Eliminacje (Tavily) | — | 7 dni (RAG) |
| Wiadomości (Tavily) | — | 6h (RAG) |

---

## Struktura bazy danych (SQLite)

```sql
PredictionHistory   -- historia zapytań (xG, Claude analysis)
ApiCache            -- cache football-data.org (klucz + ExpiresAt)
RagDocuments        -- baza wiedzy FTS5 (DocType, EntityId, TTL)
```

---

## free tier football-data.org

Plan bezpłatny udostępnia:

- MŚ 2026 (`WC`) – mecze bieżącego turnieju
- Euro 2024 (`EC`) – historia 2024
- Scorers z top lig: `PL · BL1 · SA · PD · FL1 · DED · PPL`

**Niedostępne** na free tier: WC 2022, WCQ/ECQL (eliminacje), inne historyczne turnieje.  
Eliminacje i towarzyskie są pobierane przez **Tavily** i cache'owane 7 dni.

---

## Zmienne środowiskowe – pełna lista

| Zmienna | Domyślna | Opis |
|---|---|---|
| `FOOTBALL_DATA_API_KEY` | *(wymagane)* | Klucz football-data.org |
| `FOOTBALL_DATA_BASE_URL` | `https://api.football-data.org/v4` | Base URL API |
| `FOOTBALL_DATA_RATE_LIMIT_PER_MINUTE` | `10` | Max req/min |
| `FOOTBALL_DATA_CACHE_SECONDS` | `120` | TTL memory cache |
| `TAVILY_API_KEY` | *(opcjonalne)* | Klucz Tavily Search |
| `TAVILY_MAX_RESULTS` | `5` | Liczba wyników |
| `TAVILY_SEARCH_DEPTH` | `basic` | `basic` lub `advanced` |
| `ANTHROPIC_API_KEY` | *(opcjonalne)* | Klucz Anthropic API |
| `CLAUDE_MODEL` | `claude-sonnet-4-6` | Model Claude |
| `CLAUDE_MAX_TOKENS` | `2000` | Maks. długość odpowiedzi |
| `CLAUDE_TEMPERATURE` | `0.3` | Temperatura (0.0–1.0) |
| `DATABASE_PATH` | `./mundial2026.db` | Ścieżka do pliku SQLite |
| `RAG_TOP_K` | `5` | Liczba dokumentów RAG |
| `ASPNETCORE_ENVIRONMENT` | `Development` | Środowisko .NET |

---

## Drużyny

20 drużyn MŚ 2026 z danymi statystycznymi, zawodnikami i profilami:

**Europa:** Francja · Niemcy · Anglia · Hiszpania · Portugalia · Holandia · Belgia · Włochy · Polska  
**Ameryka Płd.:** Brazylia · Argentyna · Urugwaj  
**Ameryka Płn.:** USA · Meksyk · Kanada  
**Azja/Afryka:** Japonia · Korea Płd. · Senegal · Maroko · Arabia Saudyjska

---

## Licencja

Projekt prywatny / edukacyjny.  
Dane football-data.org podlegają [warunkom licencji football-data.org](https://www.football-data.org/coverage).
