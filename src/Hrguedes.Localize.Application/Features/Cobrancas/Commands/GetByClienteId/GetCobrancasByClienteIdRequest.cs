using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetByClienteId;

public class GetCobrancasByClienteIdRequest : PaginationRequest, IRequest<HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>>
{
    public int ClienteId { get; set; }
}