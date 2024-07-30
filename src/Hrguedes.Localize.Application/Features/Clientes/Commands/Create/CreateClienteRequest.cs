using Hrguedes.Localize.Application.Features.Clientes.Validations;
using Hrguedes.Localize.Infra.Shared.FluentValidation;
using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;
using Newtonsoft.Json;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.Create;

public class CreateClienteRequest : RequestValidation, IRequest<HttpResult<CreateClienteResponse>>
{
    [JsonConstructor]
    public CreateClienteRequest(string nome, string documento, string telefone, string endereco)
    {
        Nome = nome;
        Documento = documento;
        Telefone = telefone;
        Endereco = endereco;
        Validate(this, new CreateClienteValidation());
    }

    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    
    [JsonIgnore]
    public Guid UsuarioId { get; set; }
}