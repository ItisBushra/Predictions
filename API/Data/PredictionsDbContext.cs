using API.Models;
using Microsoft.EntityFrameworkCore;

public class PredictionsDbContext : DbContext
{
    public PredictionsDbContext(DbContextOptions<PredictionsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FutureResults>(entity =>
        {
            entity.ToTable("Future_Results", t => t.ExcludeFromMigrations());
            entity.HasKey(e => e.Id);
        });
    }
    public DbSet<FutureResults> Future_Results { get; set; }
}
