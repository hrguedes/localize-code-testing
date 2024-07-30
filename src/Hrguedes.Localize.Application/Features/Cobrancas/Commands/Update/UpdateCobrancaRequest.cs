using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.Update;

public class UpdateCobrancaRequest : IRequest<HttpResult<UpdateCobrancaResponse>>
{
    public int Id { get; set; }
    public bool Pago { get; set; }
    public decimal Valor { set; get; }
    public string Descricao { get; set; } = null!;
    public DateTime DataVencimento { get; set; }
}