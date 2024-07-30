using System.Reflection;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Infra.DependencyInjection;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hrguedes.Localize.Application.DependencyInjection;

public static class AddDependencyInjection
{
    public static IServiceCollection AddApplicationInjection(this IServiceCollection services, IConfiguration config)
    {
        // infra
        services.AddInfraInjection(config);

        // handlers
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(GetUsuarioByEmailHandler).Assembly);
        });

        // mapping
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        return services;
    }
}