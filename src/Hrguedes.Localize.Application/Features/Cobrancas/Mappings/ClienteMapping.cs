using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Create;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Update;
using Hrguedes.Localize.Application.Features.Cobrancas.Models;
using Hrguedes.Localize.Domain.Entities;
using Mapster;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Mappings;

public class ClienteMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCobrancaRequest, Cobranca>().IgnoreNullValues(true);
        config.NewConfig<UpdateCobrancaRequest, Cobranca>().IgnoreNullValues(true);
        config.NewConfig<Cobranca, CobrancaModel>()
            .Map(dest => dest.RegistroCriado, sourc => sourc.RegistroCriado.ToString("dd/MM/yyyy"))
            .Map(dest => dest.RegistroRemovido, sourc => sourc.RegistroRemovido!.Value.ToString("dd/MM/yyyy"))
            .Map(dest => dest.UltimaAtualizacao, sourc => sourc.UltimaAtualizacao!.Value.ToString("dd/MM/yyyy"))
            .Map(dest => dest.DataVencimento, sourc => sourc.DataVencimento.ToString("dd/MM/yyyy"))
            .IgnoreNullValues(true);
    }
}