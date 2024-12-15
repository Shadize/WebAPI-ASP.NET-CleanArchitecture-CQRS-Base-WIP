

namespace WebAPI.Application.Interfaces
{
    public interface ICacheable
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        int SlidingExpirationInMinutes { get; }
        int AbsoluteExpirationInMinutes { get; }
    }
}
