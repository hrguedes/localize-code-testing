using Hrguedes.Localize.Application.Features.Clientes.Commands.Create;
using Hrguedes.Localize.Application.Features.Clientes.Commands.Update;
using Hrguedes.Localize.Application.Features.Clientes.Models;
using Hrguedes.Localize.Domain.Entities;
using Mapster;

namespace Hrguedes.Localize.Application.Features.Clientes.Mappings;

public class ClienteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateClienteRequest, Cliente>().IgnoreNullValues(true);
        config.NewConfig<UpdateClienteRequest, Cliente>()
            .IgnoreNullValues(true);
        config.NewConfig<Cliente, ClienteModel>()
            .Map(dest => dest.RegistroCriado, sourc => sourc.RegistroCriado.ToString("dd/MM/yyyy"))
            .Map(dest => dest.RegistroRemovido, sourc => sourc.RegistroRemovido!.Value.ToString("dd/MM/yyyy"))
            .Map(dest => dest.UltimaAtualizacao, sourc => sourc.UltimaAtualizacao!.Value.ToString("dd/MM/yyyy"))
            .IgnoreNullValues(true);
    }
}