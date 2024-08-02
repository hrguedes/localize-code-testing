using Hrguedes.Localize.Application.Features.Clientes.Commands.Create;
using Hrguedes.Localize.Application.Features.Clientes.Commands.Update;
using Hrguedes.Localize.Application.Features.Clientes.Models;
using Hrguedes.Localize.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Hrguedes.Localize.Application.Features.Clientes.Mappings;

public static class ClienteMapping
{
    public static void RegisterClienteMapping(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateClienteRequest, Cliente>.NewConfig().IgnoreNullValues(true);
        TypeAdapterConfig<UpdateClienteRequest, Cliente>.NewConfig()
            .IgnoreNullValues(true);
        TypeAdapterConfig<Cliente, ClienteModel>.NewConfig()
            .Map(dest => dest.RegistroCriado, sourc => sourc.RegistroCriado.ToString("dd/MM/yyyy"))
            .Map(dest => dest.RegistroRemovido, sourc => sourc.RegistroRemovido!.Value.ToString("dd/MM/yyyy"))
            .Map(dest => dest.UltimaAtualizacao, sourc => sourc.UltimaAtualizacao!.Value.ToString("dd/MM/yyyy"))
            .IgnoreNullValues(true);
    }
}