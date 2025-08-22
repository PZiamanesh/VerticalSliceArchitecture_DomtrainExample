using TravelInspiration.API.Shared.Common;
using TravelInspiration.API.Shared.Domain.Entities;

namespace TravelInspiration.API.Shared.Domain.Events;

public sealed class StopCreatedEvent : DomainEvent
{
    public Stop Stop { get; }

    public StopCreatedEvent(Stop stop)
    {
        Stop = stop;
    }
}