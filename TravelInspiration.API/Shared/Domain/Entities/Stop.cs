using TravelInspiration.API.Features.Stops;
using TravelInspiration.API.Shared.Common;
using TravelInspiration.API.Shared.Domain.Events;

namespace TravelInspiration.API.Shared.Domain.Entities;

public sealed class Stop : AuditableEntity, IHasDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Uri? ImageUri { get; set; }
    public int ItineraryId { get; set; }
    public Itinerary Itinerary { get; set; }
    public IList<DomainEvent> DomainEvents { get; } = [];
    public bool IsSuggested { get; set; }

    public Stop(string name)
    {
        Name = name;
    }

    public void HandleCreateStopCommand(CreateStopCommand command)
    {
        ImageUri = command.ImageUri is null ? null : new Uri(command.ImageUri);
        ItineraryId = command.ItineraryId;

        DomainEvents.Add(new StopCreatedEvent(this));
    }
}
