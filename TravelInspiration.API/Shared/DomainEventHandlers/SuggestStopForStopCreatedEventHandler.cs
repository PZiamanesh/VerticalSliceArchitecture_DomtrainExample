using MediatR;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Domain.Events;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API.Shared.DomainEventHandlers;

public sealed class SuggestStopForStopCreatedEventHandler : INotificationHandler<StopCreatedEvent>
{
    private readonly TravelInspirationDbContext _dbContext;
    private readonly ILogger<SuggestStopForStopCreatedEventHandler> _logger;

    public SuggestStopForStopCreatedEventHandler(
        TravelInspirationDbContext dbContext,
        ILogger<SuggestStopForStopCreatedEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Task Handle(StopCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Listener {listener} to domain event {domainEvent} triggered.",
            GetType().Name,
            notification.GetType().Name);

        // do some logic

        var suggestedStop = new Stop("AI suggested this stop")
        {
            ImageUri = new Uri("https://www.google.com"),
            ItineraryId = notification.Stop.ItineraryId,
            IsSuggested = true
        };

        _dbContext.Stops.Add(suggestedStop);
        return Task.CompletedTask;
    }
}
