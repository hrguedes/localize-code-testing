using Hrguedes.Localize.Infra.Shared.Models;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Models;

public class CobrancaModel : BaseModel<int>
{
    public bool Pago { get; set; }
    public decimal Valor { set; get; }
    public string Descricao { get; set; } = null!;
    public string DataVencimento { get; set; } = null!;
    public int ClienteId { get; set; }
}