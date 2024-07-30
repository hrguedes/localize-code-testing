using FluentValidation;
using Hrguedes.Localize.Application.Features.Clientes.Commands.Create;
using Hrguedes.Localize.Infra.Shared.Resources;

namespace Hrguedes.Localize.Application.Features.Clientes.Validations;

public class CreateClienteValidation : AbstractValidator<CreateClienteRequest>
{
    public CreateClienteValidation()
    {
        RuleFor(p => p.Nome)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
        
        RuleFor(p => p.Documento)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
        
        RuleFor(p => p.Telefone)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
        
        RuleFor(p => p.Endereco)
            .NotNull()
            .NotEmpty()
            .WithMessage(DefaultResources.NaoPodeSerVazio);
    }
}