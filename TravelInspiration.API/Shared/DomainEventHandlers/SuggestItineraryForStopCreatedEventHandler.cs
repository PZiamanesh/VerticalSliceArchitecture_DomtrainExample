using MediatR;
using TravelInspiration.API.Shared.Domain.Events;

namespace TravelInspiration.API.Shared.DomainEventHandlers;

public sealed class SuggestItineraryForStopCreatedEventHandler : INotificationHandler<StopCreatedEvent>
{
    private readonly ILogger<SuggestItineraryForStopCreatedEventHandler> _logger;

    public SuggestItineraryForStopCreatedEventHandler(
        ILogger<SuggestItineraryForStopCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(StopCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Listener {listener} to domain event {domainEvent} triggered.",
            GetType().Name,
            notification.GetType().Name);

        // do some logic

        return Task.CompletedTask;
    }
}
