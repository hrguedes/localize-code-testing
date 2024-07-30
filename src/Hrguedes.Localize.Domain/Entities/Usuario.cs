namespace Hrguedes.Localize.Domain.Entities;

public class Usuario :  BaseEntity<Guid>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public virtual List<Cliente> Clientes { get; set; } = [];
}