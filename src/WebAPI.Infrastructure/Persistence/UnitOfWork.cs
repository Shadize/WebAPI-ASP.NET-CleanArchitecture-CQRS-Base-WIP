using WebAPI.Domain.Interfaces;
using WebAPI.Infrastructure.Persistence.Repositories;

namespace WebAPI.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Examples = new ExampleRepository(context);
        }

        public IExampleRepository Examples { get; private set; }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
