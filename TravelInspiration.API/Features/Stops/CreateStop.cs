using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Common;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;
using TravelInspiration.API.Shared.Slices;
using static TravelInspiration.API.Features.Stops.CreateStopCommandHandler;

namespace TravelInspiration.API.Features.Stops;

public sealed class CreateStop : ISlice
{
    public void AddEndPoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapPost(
            "api/itineraries/{itineraryId}/stops", async (
            int itineraryId,
            CreateStopCommand createCommand,
            CancellationToken cancellationToken,
            IMediator mediator) =>
        {
            var response = await mediator.Send(createCommand);

            if (response.ResponseData is null || response.HasError)
            {
                return Results.Problem(detail: response.ErrorMessage, statusCode: response.ErrorCode);
            }

            return Results.Created($"api/itineraries/{itineraryId}/stops/{response.ResponseData.Id}", response.ResponseData);
        });
    }
}

public sealed class CreateStopCommand : IRequest<CreateStopResponse>
{
    public required string Name { get; set; }
    public Uri? ImageUri { get; set; }
    public required int ItineraryId { get; set; }
}

public sealed class CreateStopResponse : IMessageResponse<CreateStopDto>
{
    public CreateStopDto? ResponseData { get; set; }
    public bool HasError { get; set; }
    public short ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

public sealed class CreateStopCommandHandler : IRequestHandler<CreateStopCommand, CreateStopResponse>
{
    private readonly TravelInspirationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateStopCommandHandler(
        TravelInspirationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CreateStopResponse> Handle(CreateStopCommand command, CancellationToken cancellationToken)
    {
        if (!await _dbContext.Itineraries.AnyAsync(i => i.Id == command.ItineraryId))
        {
            return new CreateStopResponse { HasError = true, ErrorCode = 404, ErrorMessage = "Itinerary not found." };
        }

        var stop = new Stop(command.Name)
        {
            ImageUri = command.ImageUri,
            ItineraryId = command.ItineraryId
        };

        _dbContext.Stops.Add(stop);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateStopResponse
        {
            ResponseData = _mapper.Map<CreateStopDto>(stop)
        };
    }
}

public sealed class CreateStopDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Uri? ImageUri { get; set; }
    public required int ItineraryId { get; set; }
}

public sealed class CreateStopMapProfile : Profile
{
    public CreateStopMapProfile()
    {
        CreateMap<Stop, CreateStopDto>();
    }
}