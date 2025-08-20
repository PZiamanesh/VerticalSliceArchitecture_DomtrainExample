using Microsoft.Extensions.Diagnostics.Metrics;
using System.Diagnostics.Metrics;

namespace TravelInspiration.API.Shared.Metrics;

public class HandlerPerformanceMetric
{
    private readonly Counter<long> _handlerExecutionElapsedTimeMiliseconds;

    public HandlerPerformanceMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("TravelInspiration.Api");

        _handlerExecutionElapsedTimeMiliseconds = meter.CreateCounter<long>("TravelInspiration.Api.HandlerExecutionElapsedTime.Miliseconds",
            description: "Tracks the execution time of MediatR handlers in milliseconds");
    }

    public void RecordHandlerExecutionTime(long elapsedMilliseconds)
    {
        _handlerExecutionElapsedTimeMiliseconds.Add(elapsedMilliseconds);
    }
}
