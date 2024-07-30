using Hrguedes.Localize.Infra.Shared.Models;

namespace Hrguedes.Localize.Application.Features.Clientes.Models;

public class ClienteModel : BaseModel<int>
{
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public Guid UsuarioId { get; set; }
}