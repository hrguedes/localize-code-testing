using System.Text.Json.Serialization;
using Hrguedes.Localize.Application.Features.Cobrancas.Validations;
using Hrguedes.Localize.Infra.Shared.FluentValidation;
using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.Create;

public class CreateCobrancaRequest : RequestValidation, IRequest<HttpResult<CreateCobrancaResponse>>
{
    [JsonConstructor]
    public CreateCobrancaRequest(bool pago, decimal valor, string descricao, DateTime dataVencimento, int clienteId)
    {
        Pago = pago;
        Valor = valor;
        Descricao = descricao;
        DataVencimento = dataVencimento;
        ClienteId = clienteId;
        Validate(this, new CreateCobrancaValidation());
    }

    public bool Pago { get; set; } = false;
    public decimal Valor { set; get; }
    public string Descricao { get; set; } = null!;
    public DateTime DataVencimento { get; set; }
    public int ClienteId { get; set; }
}