using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using WebAPI.Application.Interfaces;

namespace WebAPI.Application.Behaviors
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICacheInvalidator
    {
        private readonly ILogger<CacheInvalidationBehavior<TRequest, TResponse>> _logger;
        private readonly IDistributedCache _cache;

        public CacheInvalidationBehavior(ILogger<CacheInvalidationBehavior<TRequest, TResponse>> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            await _cache.RemoveAsync(request.CacheKey, cancellationToken);
            _logger.LogInformation("Cache invalidated for key: {CacheKey}", request.CacheKey);

            return response;
        }
    }
}
