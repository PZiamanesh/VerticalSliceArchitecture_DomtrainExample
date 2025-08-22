using System.Reflection;

namespace TravelInspiration.API.Shared.Common;

public static class SliceServiceExtentions
{
    public static IServiceCollection AddSlicesSingelton(this IServiceCollection services)
    {
        var slices = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(ISlice).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface && t.IsPublic);

        foreach (var slice in slices)
        {
            services.AddSingleton(typeof(ISlice), slice);
        }

        return services;
    }

    public static IEndpointRouteBuilder UseSlices(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        foreach (ISlice slice in endpointRouteBuilder.ServiceProvider.GetServices<ISlice>())
        {
            slice.AddEndPoints(endpointRouteBuilder);
        }

        return endpointRouteBuilder;
    }
}
