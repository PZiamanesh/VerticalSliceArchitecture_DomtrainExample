using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TravelInspiration.API.Shared.Behaviours;
using TravelInspiration.API.Shared.Common;
using TravelInspiration.API.Shared.Metrics;
using TravelInspiration.API.Shared.Networking;
using TravelInspiration.API.Shared.Persistence;

namespace TravelInspiration.API;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDestinationSearchApiClient, DestinationSearchApiClient>();

        services.AddAutoMapper(ops =>
        {
            ops.AddMaps(typeof(ServiceCollectionExtensions).Assembly);
        });

        services.AddMediatR(ops =>
        {
            ops.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            ops.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
            ops.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            ops.AddOpenBehavior(typeof(HandlerPerformanceMetricBehaviour<,>));
        });

        services.AddSlicesSingelton();

        services.AddSingleton(typeof(HandlerPerformanceMetric));

        services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions));

        return services;
    }

    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TravelInspirationDbContext>(ops =>
        {
            ops.UseSqlServer(configuration["ConnectionStrings:TravelInspirationDb"]);
        });

        return services;
    }
}
