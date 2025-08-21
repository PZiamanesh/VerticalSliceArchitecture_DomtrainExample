using FluentValidation;
using MediatR;
using TravelInspiration.API.Shared.Common;

namespace TravelInspiration.API.Shared.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IMessageResponse, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationContext = new ValidationContext<TRequest>(request);

        var validationErrors = _validators
            .Select(v => v.Validate(validationContext))
            .Where(vr => vr.IsValid == false)
            .SelectMany(v => v.Errors)
            .ToList();

        if (validationErrors is null || !validationErrors.Any())
        {
            return await next();
        }

        var errorDic = (from error in validationErrors
                        group error by error.PropertyName into g
                        select new
                        {
                            PropertyName = g.Key,
                            Erros = g.Select(e => e.ErrorMessage).ToList()
                        }).ToDictionary(i => i.PropertyName, i => i.Erros.ToArray());

        return new TResponse { ValidationErrors = errorDic, StatusCode = 400 };
    }
}
