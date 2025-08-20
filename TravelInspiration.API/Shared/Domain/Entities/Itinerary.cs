namespace TravelInspiration.API.Shared.Domain.Entities;

public sealed class Itinerary : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string UserId { get; set; }
    public ICollection<Stop> Stops { get; set; } = [];

    public Itinerary(string name, string userId)
    {
        Name = name;
        UserId = userId;
    }
}