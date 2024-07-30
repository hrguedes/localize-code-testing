using FluentValidation;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Create;
using Hrguedes.Localize.Infra.Shared.Resources;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Validations;

public class CreateCobrancaValidation : AbstractValidator<CreateCobrancaRequest>
{
    public CreateCobrancaValidation()
    {
        RuleFor(p => p.Valor)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
       
        RuleFor(p => p.Descricao)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
        
        RuleFor(p => p.DataVencimento)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
        
        RuleFor(p => p.ClienteId)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
    }
}