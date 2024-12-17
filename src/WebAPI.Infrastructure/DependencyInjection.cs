using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Application.Interfaces;
using WebAPI.Infrastructure.Extensions;
using WebAPI.Infrastructure.Persistence;
using WebAPI.Infrastructure.Settings;

namespace WebAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddServices()
                .AddAuthentication(configuration)
                .AddAuthorization()
                .AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WebAPI.Infrastructure"))
            );

            services.AddDistributedMemoryCache();


            return services;
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            return services;
        }
        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            services
                .ConfigureOptions<JwtBearerTokenConfiguration>()
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();


            return services;
        }
    }
}
