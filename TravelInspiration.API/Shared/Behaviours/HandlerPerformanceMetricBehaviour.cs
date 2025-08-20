using MediatR;
using System.Diagnostics;
using TravelInspiration.API.Shared.Metrics;

namespace TravelInspiration.API.Shared.Behaviours;

internal class HandlerPerformanceMetricBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly HandlerPerformanceMetric _handlerPerformanceMetric;
    private readonly Stopwatch _timer = new();

    public HandlerPerformanceMetricBehaviour(HandlerPerformanceMetric handlerPerformanceMetric)
    {
        _handlerPerformanceMetric = handlerPerformanceMetric;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = next();
        _timer.Stop();

        _handlerPerformanceMetric.RecordHandlerExecutionTime(_timer.ElapsedMilliseconds);

        return response;
    }
}