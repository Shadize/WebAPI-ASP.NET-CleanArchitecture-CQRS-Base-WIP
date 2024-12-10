

namespace WebAPI.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IExampleRepository Examples { get; }
        Task<int> CompleteAsync();
    }
}
