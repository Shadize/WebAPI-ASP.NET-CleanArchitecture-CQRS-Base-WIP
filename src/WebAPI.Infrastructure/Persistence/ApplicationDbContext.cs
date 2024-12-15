

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Entities;
using WebAPI.Infrastructure.Identity;

namespace WebAPI.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
      {
        
      }
      public DbSet<Example> Examples { get; set; }
    }
}
