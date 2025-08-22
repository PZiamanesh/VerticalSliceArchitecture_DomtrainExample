using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.Shared.Persistence.Configurations;

public class StopConfiguration : IEntityTypeConfiguration<Stop>
{
    public void Configure(EntityTypeBuilder<Stop> builder)
    {
        builder.ToTable("Stops");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .UseIdentityColumn();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.ImageUri)
            .HasConversion(
                v => v.ToString(),
                v => string.IsNullOrEmpty(v) ? null : new Uri(v));

        builder.Property(s => s.IsSuggested)
            .HasDefaultValue(false);

        builder.Ignore(s => s.DomainEvents);

        builder.HasOne(s => s.Itinerary)
            .WithMany(i => i.Stops)
            .HasForeignKey(s => s.ItineraryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
