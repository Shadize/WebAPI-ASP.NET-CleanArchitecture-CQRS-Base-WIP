
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;

namespace WebAPI.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Example> Examples { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
