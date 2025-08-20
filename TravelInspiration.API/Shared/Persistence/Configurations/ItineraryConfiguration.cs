using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.Shared.Persistence.Configurations;

public class ItineraryConfiguration : IEntityTypeConfiguration<Itinerary>
{
    public void Configure(EntityTypeBuilder<Itinerary> builder)
    {
        builder.ToTable("Itineraries");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).UseIdentityColumn();

        builder.Property(i => i.Name).IsRequired().HasMaxLength(200);

        builder.Property(i => i.Description).HasMaxLength(2500);

        builder.Property(i => i.UserId).IsRequired().HasMaxLength(100);
    }
}
