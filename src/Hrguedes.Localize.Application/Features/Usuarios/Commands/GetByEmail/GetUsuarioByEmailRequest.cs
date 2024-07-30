using Hrguedes.Localize.Infra.Shared.FluentValidation;
using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;

public sealed class GetUsuarioByEmailRequest(string email) : RequestValidation, IRequest<HttpResult<GetUsuarioByEmailResponse>>
{
    public string Email { get; set; } = email;
}