using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Hrguedes.Localize.Infra.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraInjection(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataModule(config);
        return services;
    }

    private static void AddDataModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection"),
                c => c.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped(x =>
        {
            return new DbSession(config.GetConnectionString("DefaultConnection") ?? "");
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}