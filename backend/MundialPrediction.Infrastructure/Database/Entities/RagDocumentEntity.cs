namespace MundialPrediction.Infrastructure.Database.Entities;

public class RagDocumentEntity
{
    public int Id { get; set; }

    // Kim lub czym dotyczy dokument
    public string EntityId { get; set; } = string.Empty;   // np. "fra", "mbappe", "fra-vs-arg"
    public string DocType { get; set; } = string.Empty;    // "team_news" | "injury_report" | "head_to_head" | "player_profile" | "match_preview"

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;    // Treść do szukania

    public string? SourceUrl { get; set; }                 // Źródło (Tavily)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ValidUntil { get; set; }              // null = stałe, data = wygasa
}
