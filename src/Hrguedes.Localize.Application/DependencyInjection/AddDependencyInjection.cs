using System.Reflection;
using Hrguedes.Localize.Application.Features.Clientes.Mappings;
using Hrguedes.Localize.Application.Features.Cobrancas.Mappings;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Application.Features.Usuarios.Mappings;
using Hrguedes.Localize.Infra.DependencyInjection;
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
        services.RegisterCobrancaMapping();
        services.RegisterClienteMapping();
        services.RegisterUsuarioMapping();

        
        return services;
    }
    
}