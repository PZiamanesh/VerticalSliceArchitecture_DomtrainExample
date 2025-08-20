using MediatR.Pipeline;

namespace TravelInspiration.API.Shared.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Endpoint called: {SliceEndpointName}", typeof(TRequest).Name);

        return Task.CompletedTask;
    }
}
