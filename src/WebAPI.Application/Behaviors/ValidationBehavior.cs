using FluentResults;
using FluentValidation;
using MediatR;

namespace WebAPI.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

                var failures = validationResults
                    .Where(r => r.Errors.Count > 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Count > 0)
                {
                    var errors = failures
                        .Select(f => new FluentResults.Error(f.ErrorMessage))
                        .ToList();

                    if (typeof(TResponse).IsAssignableTo(typeof(Result)))
                    {
                        var resultType = typeof(TResponse);
                        var resultInstance = Activator.CreateInstance(resultType) as Result;
                        resultInstance.WithErrors(errors);
                        return (TResponse)(object)resultInstance;
                    }
                }
            }

            return await next().ConfigureAwait(false);
        }

    }
}
