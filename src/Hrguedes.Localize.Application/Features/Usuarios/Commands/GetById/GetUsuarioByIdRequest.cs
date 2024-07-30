using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Usuarios.Commands.GetById;

public class GetUsuarioByIdRequest : IRequest<HttpResult<GetUsuarioByIdResponse>>
{
    public Guid Id { get; set; }
}