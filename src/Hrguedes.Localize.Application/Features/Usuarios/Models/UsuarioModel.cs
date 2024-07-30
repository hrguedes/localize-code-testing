using Hrguedes.Localize.Infra.Shared.Models;

namespace Hrguedes.Localize.Application.Features.Usuarios.Models;

public class UsuarioModel : BaseModel<Guid>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public Guid UsuarioId { get; set; }
}