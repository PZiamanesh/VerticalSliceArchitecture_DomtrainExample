using MediatR;

namespace TravelInspiration.API.Shared.Common;

public abstract class DomainEvent : INotification
{
    public bool IsPublished { get; set; }
    public DateTime OccuredOn => DateTime.UtcNow;
}