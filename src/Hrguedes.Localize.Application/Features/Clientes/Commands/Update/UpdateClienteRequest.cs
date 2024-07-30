using System.Text.Json.Serialization;
using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.Update;

public class UpdateClienteRequest : IRequest<HttpResult<UpdateClienteResponse>>
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Documento { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
}