namespace Hrguedes.Localize.Domain.Entities;

public class Cobranca : BaseEntity<int>
{
    
    public bool Pago { get; set; }
    public decimal Valor { set; get; }
    public string Descricao { get; set; } = null!;
    public DateTime DataVencimento { get; set; }
    public int ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; } = null!;
}