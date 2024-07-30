namespace Hrguedes.Localize.Domain.Entities;

public class Cliente :  BaseEntity<int>
{
    public string Nome { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public Guid UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = default!;
    public virtual List<Cobranca> Cobrancas { get; set; } = [];
}