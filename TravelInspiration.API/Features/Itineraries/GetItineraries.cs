using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using TravelInspiration.API.Shared.Common;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;

namespace TravelInspiration.API.Features.Itineraries;

public sealed class GetItineraries : ISlice
{
    public void AddEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/itineraries", async (
            string? searchFor,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            var response = await mediator.Send(new GetItinerariesQuery
            {
                SearchFor = searchFor
            }, cancellationToken);

            if (response.HasError)
            {
                return ProblemDetailResult.ProblemDetail(response);
            }

            return Results.Ok(response.ResponseData);
        });
    }
}

public sealed class GetItinerariesQuery : IRequest<GetItinerariesResponse>
{
    public string? SearchFor { get; init; }
}

public sealed class GetItinerariesResponse : Response<IEnumerable<ItinerariDto>>
{
    public override IEnumerable<ItinerariDto> ResponseData { get; set; }
}

public sealed class GetItinerariesQueryHandler : IRequestHandler<GetItinerariesQuery, GetItinerariesResponse>
{
    private readonly TravelInspirationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetItinerariesQueryHandler(
        TravelInspirationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetItinerariesResponse> Handle(GetItinerariesQuery request, CancellationToken cancellationToken)
    {
        var itineraries = await _dbContext.Itineraries
                .AsNoTracking()
                .Where(i => request.SearchFor == null ||
                            i.Name.Contains(request.SearchFor) ||
                            (i.Description != null && i.Description.Contains(request.SearchFor)))
                .ToListAsync(cancellationToken);

        var itinerariesDto = _mapper.Map<IEnumerable<ItinerariDto>>(itineraries);

        return new GetItinerariesResponse
        {
            ResponseData = itinerariesDto
        };
    }
}

public sealed class ItinerariDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string UserId { get; set; }
}

public sealed class ItineraryMapProfile : Profile
{
    public ItineraryMapProfile()
    {
        CreateMap<Itinerary, ItinerariDto>();
    }
}