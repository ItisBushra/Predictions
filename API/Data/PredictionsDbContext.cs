using API.Models;
using Microsoft.EntityFrameworkCore;

public class PredictionsDbContext : DbContext
{
    public PredictionsDbContext(DbContextOptions<PredictionsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SafetyTrends>(entity =>
        {
            entity.ToTable("Safety_Trends", t => t.ExcludeFromMigrations());
            entity.HasKey(e => e.Id);
        });
    }
    public DbSet<SafetyTrends> Safety_Trends { get; set; }
}
