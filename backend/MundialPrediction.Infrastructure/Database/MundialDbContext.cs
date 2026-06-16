using Microsoft.EntityFrameworkCore;
using MundialPrediction.Infrastructure.Database.Entities;

namespace MundialPrediction.Infrastructure.Database;

public class MundialDbContext : DbContext
{
    public MundialDbContext(DbContextOptions<MundialDbContext> options) : base(options) { }

    public DbSet<PredictionHistoryEntity> PredictionHistory => Set<PredictionHistoryEntity>();
    public DbSet<ApiCacheEntity> ApiCache => Set<ApiCacheEntity>();
    public DbSet<RagDocumentEntity> RagDocuments => Set<RagDocumentEntity>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<PredictionHistoryEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.HomeTeamId, x.AwayTeamId });
            e.HasIndex(x => x.CreatedAt);
        });

        mb.Entity<ApiCacheEntity>(e =>
        {
            e.HasKey(x => x.CacheKey);
            e.HasIndex(x => x.ExpiresAt);
        });

        mb.Entity<RagDocumentEntity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.EntityId);
            e.HasIndex(x => x.DocType);
        });
    }

    // Tworzy SQLite FTS5 virtual table dla RAG search – wywoływane przy inicjalizacji
    public async Task EnsureFts5TableAsync()
    {
        await Database.ExecuteSqlRawAsync("""
            CREATE VIRTUAL TABLE IF NOT EXISTS RagFts USING fts5(
                doc_id UNINDEXED,
                content,
                tokenize='unicode61'
            );
        """);

        // Trigger: przy INSERT do RagDocuments automatycznie dodaj do FTS
        await Database.ExecuteSqlRawAsync("""
            CREATE TRIGGER IF NOT EXISTS rag_ai AFTER INSERT ON RagDocuments BEGIN
                INSERT INTO RagFts(doc_id, content) VALUES (new.Id, new.Content);
            END;
        """);

        // Trigger: przy DELETE usuń z FTS
        await Database.ExecuteSqlRawAsync("""
            CREATE TRIGGER IF NOT EXISTS rag_ad AFTER DELETE ON RagDocuments BEGIN
                DELETE FROM RagFts WHERE doc_id = old.Id;
            END;
        """);
    }
}
