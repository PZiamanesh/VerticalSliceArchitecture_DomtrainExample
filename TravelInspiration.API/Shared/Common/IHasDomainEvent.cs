namespace TravelInspiration.API.Shared.Common;

public interface IHasDomainEvent
{
    IList<DomainEvent> DomainEvents { get; }
}
