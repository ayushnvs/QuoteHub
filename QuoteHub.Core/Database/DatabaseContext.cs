using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuoteHub.Core.Entities.Models;

namespace QuoteHub.Core.Database;

public class DatabaseContext : DbContext
{
    public DbSet<AuthorDBO> Authors { get; set; }
    public DbSet<QuoteDBO> Quotes { get; set; }
    public DbSet<LanguageDBO> Languages { get; set; }


    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the entity properties and relationships here
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseDBO &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (EntityEntry entry in entries)
        {
            BaseDBO entity = (BaseDBO)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedOn = DateTime.Now;
            }

            entity.UpdatedOn = DateTime.Now;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
