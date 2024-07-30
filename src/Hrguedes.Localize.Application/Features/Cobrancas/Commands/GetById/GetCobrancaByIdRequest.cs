using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetById;

public class GetCobrancaByIdRequest : IRequest<HttpResult<GetCobrancaByIdResponse>>
{
    public int CobrancaId { get; set; }
}