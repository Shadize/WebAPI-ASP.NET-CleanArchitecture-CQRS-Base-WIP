using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Interfaces;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Identity;

namespace WebAPI.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
        
      }
      public DbSet<Example> Examples => Set<Example>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
