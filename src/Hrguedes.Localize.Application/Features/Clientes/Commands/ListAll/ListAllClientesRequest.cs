using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;
using Newtonsoft.Json;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.ListAll;

public class ListAllClientesRequest : PaginationRequest, IRequest<HttpResult<PaginationResponse<ListAllClientesResponse>>>
{
    public string? Pesquisa { get; set; }
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
}