using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;
using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.Shared.Persistence;

public class TravelInspirationDbContext : DbContext
{
    public DbSet<Stop> Stops { get; set; }
    public DbSet<Itinerary> Itineraries { get; set; }

    public TravelInspirationDbContext(DbContextOptions<TravelInspirationDbContext> ops) : base(ops)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region seed
        modelBuilder.Entity<Itinerary>().HasData(
            new Itinerary("A Trip to Paris", "dummyuserid")
            {
                Id = 1,
                Description = "Five great days in Paris",
                CreatedBy = "DATASEED",
                CreatedAt = DateTime.UtcNow
            },
             new Itinerary("Antwerp Extravaganza", "dummyuserid")
             {
                 Id = 2,
                 Description = "A week in beautiful Antwerp",
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             });

        modelBuilder.Entity<Stop>().HasData(
             new("The Eiffel Tower")
             {
                 Id = 1,
                 ItineraryId = 1,
                 ImageUri = new Uri("https://localhost:7120/images/eiffeltower.jpg"),
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             },
             new("The Louvre")
             {
                 Id = 2,
                 ItineraryId = 1,
                 ImageUri = new Uri("https://localhost:7120/images/louvre.jpg"),
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             },
             new("Père Lachaise Cemetery")
             {
                 Id = 3,
                 ItineraryId = 1,
                 ImageUri = new Uri("https://localhost:7120/images/perelachaise.jpg"),
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             },
             new("The Royal Museum of Beautiful Arts")
             {
                 Id = 4,
                 ItineraryId = 2,
                 ImageUri = new Uri("https://localhost:7120/images/royalmuseum.jpg"),
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             },
             new("Saint Paul's Church")
             {
                 Id = 5,
                 ItineraryId = 2,
                 ImageUri = new Uri("https://localhost:7120/images/stpauls.jpg"),
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             },
             new("Michelin Restaurant Visit")
             {
                 Id = 6,
                 ItineraryId = 2,
                 ImageUri = new Uri("https://localhost:7120/images/michelin.jpg"),
                 CreatedBy = "DATASEED",
                 CreatedAt = DateTime.UtcNow
             });
        #endregion

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelInspirationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "system";
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "system";
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "system";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
