using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelInspiration.API.Shared.Common;
using TravelInspiration.API.Shared.Domain.Entities;
using TravelInspiration.API.Shared.Persistence;

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
            createCommand.ItineraryId = itineraryId;

            var response = await mediator.Send(createCommand);

            if (response.HasError)
            {
                return ProblemDetailResult.ProblemDetail(response);
            }

            return Results.Created($"api/itineraries/{itineraryId}/stops/{response.ResponseData.Id}", response.ResponseData);
        });
    }
}

public sealed class CreateStopCommand : IRequest<CreateStopResponse>
{
    public string Name { get; set; }
    public string? ImageUri { get; set; }
    public int ItineraryId { get; set; }
}

public sealed class CreateStopResponse : Response<CreateStopDto>
{
    public override CreateStopDto ResponseData { get; set; }
}

public sealed class CreateStopValidator : AbstractValidator<CreateStopCommand>
{
    public CreateStopValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(i => i.ImageUri)
            .Must(i => Uri.TryCreate(i, UriKind.Absolute, out var _))
            .When(i => !string.IsNullOrEmpty(i.ImageUri))
            .WithMessage("Incorrect image uri format. Absolute path needed.");

        RuleFor(i => i.ItineraryId)
            .NotEmpty()
            .Must(i => i > 0)
            .WithMessage("ItineraryId must be greater than 0.");
    }
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
            return new CreateStopResponse { LogicError = $"Itinerary with id {command.ItineraryId} was not found.", StatusCode = 400 };
        }

        var stop = new Stop(command.Name);
        stop.HandleCreateStopCommand(command);

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
