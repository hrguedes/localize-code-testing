using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;

namespace Hrguedes.Localize.Application.Features.Usuarios.Commands.ListAll;

public class ListAllUsuariosRequest : PaginationRequest, IRequest<HttpResult<PaginationResponse<ListAllUsuariosResponse>>>
{
    public string? Pesquisa { get; set; }
}