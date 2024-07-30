using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;
using Newtonsoft.Json;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.GetById;

public class GetClienteByIdRequest : IRequest<HttpResult<GetClienteByIdResponse>>
{
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
    public int ClienteId { get; set; }
}