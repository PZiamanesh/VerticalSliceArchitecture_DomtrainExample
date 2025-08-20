using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Stops;

public sealed class GetStops : ISlice
{
    public void AddEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries/{idineraryId}/stops", async (
            int idineraryId,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            var response = await mediator.Send(new GetStopsQuery { ItineraryId = idineraryId }, cancellationToken);

            if (response.Stops is null || !response.Stops.Any())
            {
                return Results.NotFound();
            }

            return Results.Ok(response.Stops);
        });
    }
}

public sealed class GetStopsQuery : IRequest<GetStopsResponse>
{
    public required int ItineraryId { get; init; }
}

public sealed class GetStopsResponse
{
    public required IEnumerable<GetStopDto> Stops { get; init; }
}

public sealed class GetStopsHandler : IRequestHandler<GetStopsQuery, GetStopsResponse>
{
    private readonly TravelInspirationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetStopsHandler(
        TravelInspirationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetStopsResponse> Handle(GetStopsQuery request, CancellationToken cancellationToken)
    {
        var itinerary = await _dbContext.Itineraries
            .Include(i => i.Stops)
            .FirstOrDefaultAsync(i => i.Id == request.ItineraryId, cancellationToken);

        if (itinerary is null || !itinerary.Stops.Any())
        {
            return new GetStopsResponse { Stops = Enumerable.Empty<GetStopDto>() };
        }

        return new GetStopsResponse
        {
            Stops = _mapper.Map<IEnumerable<GetStopDto>>(itinerary.Stops)
        };
    }
}

public sealed class GetStopDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public Uri? ImageUri { get; set; }
    public required int ItineraryId { get; set; }
}

public sealed class GetStopMapProfile : Profile
{
    public GetStopMapProfile()
    {
        CreateMap<Stop, GetStopDto>();
    }
}