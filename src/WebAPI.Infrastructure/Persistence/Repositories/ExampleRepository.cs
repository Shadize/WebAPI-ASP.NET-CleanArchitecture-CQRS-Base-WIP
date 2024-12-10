using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Infrastructure.Persistence.Repositories
{
    public class ExampleRepository : GenericRepository<Example>, IExampleRepository
    {
        public ExampleRepository(ApplicationDbContext context) : base(context) { }
    }
}
