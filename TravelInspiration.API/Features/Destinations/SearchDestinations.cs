using TravelInspiration.API.Shared.Networking;

namespace TravelInspiration.API.Features.Destinations;

public static class SearchDestinations
{
    public static void AddEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/destinations",
            async (
                string? searchFor,
                ILoggerFactory loggerFactory,
                IDestinationSearchApiClient destinationSearchApiClient,
                CancellationToken cancellation) =>
        {
            loggerFactory.CreateLogger("EndpointHandlers").LogInformation("SearchDestinations feature called.");

            var destinations = await destinationSearchApiClient.GetDestinationsAsync(searchFor, cancellation);

            var result = destinations.Select(a => new
            {
                a.Name,
                a.Description,
                a.ImageUri
            });

            return Results.Ok(result);
        });
    }
}
