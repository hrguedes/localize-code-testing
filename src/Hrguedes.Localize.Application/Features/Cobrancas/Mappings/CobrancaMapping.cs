using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Create;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetByClienteId;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Update;
using Hrguedes.Localize.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Mappings;

public static class CobrancaMapping
{
    public static void RegisterCobrancaMapping(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateCobrancaRequest, Cobranca>.NewConfig().IgnoreNullValues(true);
        TypeAdapterConfig<UpdateCobrancaRequest, Cobranca>.NewConfig().IgnoreNullValues(true);
        TypeAdapterConfig<Cobranca, GetCobrancasByClienteIdResponse>.NewConfig()
            .Map(dest => dest.Descricao, sourc => sourc.Descricao.ToUpper())
            .Map(dest => dest.RegistroCriado, sourc => sourc.RegistroCriado.ToString("dd/MM/yyyy"))
            .Map(dest => dest.DataVencimento, sourc => sourc.DataVencimento.ToString("dd/MM/yyyy"))
            .Map(dest => dest.Vencimento, sourc => sourc.DataVencimento)
            .IgnoreNullValues(true);
    }
}