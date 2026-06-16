using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MundialPrediction.Infrastructure.Database;
using MundialPrediction.Infrastructure.Database.Entities;

namespace MundialPrediction.Infrastructure.RAG;

// Baza wiedzy oparta na SQLite FTS5.
// Przechowuje fragmenty tekstu o drużynach, zawodnikach i meczach.
// Claude korzysta z niej jako kontekstu przy generowaniu analiz.
public class KnowledgeBase
{
    private readonly MundialDbContext _db;
    private readonly ILogger<KnowledgeBase> _logger;

    public KnowledgeBase(MundialDbContext db, ILogger<KnowledgeBase> logger)
    {
        _db = db;
        _logger = logger;
    }

    // Dodaj lub zaktualizuj dokument w bazie wiedzy
    public async Task UpsertAsync(string entityId, string docType, string title, string content, string? sourceUrl = null, TimeSpan? validFor = null)
    {
        // Usuń stary dokument tego samego typu dla tej samej encji
        var existing = await _db.RagDocuments
            .Where(d => d.EntityId == entityId && d.DocType == docType)
            .ToListAsync();

        if (existing.Any())
            _db.RagDocuments.RemoveRange(existing);

        var doc = new RagDocumentEntity
        {
            EntityId = entityId,
            DocType = docType,
            Title = title,
            Content = content,
            SourceUrl = sourceUrl,
            CreatedAt = DateTime.UtcNow,
            ValidUntil = validFor.HasValue ? DateTime.UtcNow.Add(validFor.Value) : null
        };

        _db.RagDocuments.Add(doc);
        await _db.SaveChangesAsync();
        _logger.LogInformation("RAG upsert: [{DocType}] {EntityId} – {Title}", docType, entityId, title);
    }

    // Pobierz top-K dokumentów pasujących do zapytania (FTS5)
    public async Task<List<RagResult>> SearchAsync(string query, int topK = 5)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new();

        // FTS5 MATCH z rankingiem – wyższy rank (bliżej 0) = lepsiej pasuje
        var sql = """
            SELECT rd.Id, rd.EntityId, rd.DocType, rd.Title, rd.Content, rd.SourceUrl,
                   rf.rank
            FROM RagDocuments rd
            JOIN RagFts rf ON rf.doc_id = rd.Id
            WHERE RagFts MATCH {0}
              AND (rd.ValidUntil IS NULL OR rd.ValidUntil > {1})
            ORDER BY rf.rank
            LIMIT {2}
            """;

        try
        {
            var results = await _db.Database
                .SqlQueryRaw<RagResultRaw>(sql, FtsSanitize(query), DateTime.UtcNow, topK)
                .ToListAsync();

            return results.Select(r => new RagResult
            {
                EntityId = r.EntityId,
                DocType = r.DocType,
                Title = r.Title,
                Content = r.Content,
                SourceUrl = r.SourceUrl,
                Rank = r.Rank
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "FTS5 search error dla: {Query}", query);
            // Fallback: zwróć ostatnie dokumenty dla danego entityId jeśli FTS nie działa
            return new();
        }
    }

    // Pobierz dokumenty dla konkretnej encji (np. drużyny)
    public async Task<List<RagResult>> GetByEntityAsync(string entityId, string? docType = null)
    {
        var q = _db.RagDocuments
            .Where(d => d.EntityId == entityId)
            .Where(d => d.ValidUntil == null || d.ValidUntil > DateTime.UtcNow);

        if (docType != null)
            q = q.Where(d => d.DocType == docType);

        var docs = await q.OrderByDescending(d => d.CreatedAt).Take(10).ToListAsync();

        return docs.Select(d => new RagResult
        {
            EntityId = d.EntityId,
            DocType = d.DocType,
            Title = d.Title,
            Content = d.Content,
            SourceUrl = d.SourceUrl,
            Rank = 0
        }).ToList();
    }

    // Buduje string kontekstu dla Claude z pobranych dokumentów
    public static string BuildContext(IEnumerable<RagResult> results)
    {
        var chunks = results
            .Where(r => !string.IsNullOrWhiteSpace(r.Content))
            .Select(r => $"[{r.DocType} | {r.EntityId}] {r.Title}\n{r.Content}");

        return string.Join("\n\n---\n\n", chunks);
    }

    // Sanityzacja zapytania FTS5 – usuwa znaki specjalne
    private static string FtsSanitize(string query)
    {
        // FTS5 nie lubi cudzysłowów i gwiazdek bez escape
        return "\"" + query.Replace("\"", " ").Replace("*", " ") + "\"";
    }
}

public class RagResult
{
    public string EntityId { get; set; } = string.Empty;
    public string DocType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? SourceUrl { get; set; }
    public double Rank { get; set; }
}

// Wewnętrzna klasa do mapowania wyników surowego SQL
internal class RagResultRaw
{
    public int Id { get; set; }
    public string EntityId { get; set; } = string.Empty;
    public string DocType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? SourceUrl { get; set; }
    public double Rank { get; set; }
}
