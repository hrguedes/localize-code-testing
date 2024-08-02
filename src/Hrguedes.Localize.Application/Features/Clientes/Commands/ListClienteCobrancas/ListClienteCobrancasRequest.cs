using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;
using Newtonsoft.Json;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.ListClienteCobrancas;

public class ListClienteCobrancasRequest : PaginationRequest,  IRequest<HttpResult<PaginationResponse<ListClienteCobrancasResponse>>>
{
    public string? Pesquisa { get; set; }
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
}