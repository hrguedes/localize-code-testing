using Asp.Versioning;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetById;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.ListAll;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrguedes.Localize.Api.Controllers.v1.Sistema;


/// <summary>
/// Usuários
/// </summary>
[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/sistema/usuarios")]
public class UsuariosController : BaseController
{
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="logger"></param>
    public UsuariosController(ILogger<BaseController> logger) : base(logger)
    {
    }
    
    /// <summary>
    /// Obter usuários por Id
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/sistema/usuarios/439E6DCA-4754-416C-ACE9-08DBF75835C1
    /// 
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="id"></param>
    /// <returns> {} </returns>
    /// <response code="200">{}</response>
    /// <response code="400">Erro de Cliente</response>
    /// <response code="500">Erro de Servidor</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(HttpResult<GetUsuarioByIdResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<GetUsuarioByIdResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> GetById(
        [FromServices] IMediator mediator,
        [FromRoute] string id) => ResponseCode(await mediator.Send(new GetUsuarioByIdRequest { Id = Guid.Parse(id) }));

    /// <summary>
    /// Obter usuários por Email
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/sistema/usuarios?email=blabla@bla.com
    /// 
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="email"></param>
    /// <returns> {} </returns>
    /// <response code="200">{}</response>
    /// <response code="400">Erro de Cliente</response>
    /// <response code="500">Erro de Servidor</response>
    [HttpGet("email")]
    [Authorize]
    [ProducesResponseType(typeof(HttpResult<GetUsuarioByEmailResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<GetUsuarioByEmailResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> GetByEmail(
        [FromServices] IMediator mediator,
        [FromQuery] string email) => ResponseCode(await mediator.Send(new GetUsuarioByEmailRequest(email)));
    
    /// <summary>
    /// Listar usuários
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/sistema/usuarios?Pesquisa=sd&amp;Pagina=1&amp;TotalPorPagina=12
    /// 
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns> [{}] </returns>
    /// <response code="200">[{}]</response>
    /// <response code="400">Erro de Cliente</response>
    /// <response code="500">Erro de Servidor</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(HttpResult<PaginationResponse<ListAllUsuariosResponse>>), 200)]
    [ProducesResponseType(typeof(HttpResult<PaginationResponse<ListAllUsuariosResponse>>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> ListarUsuarios(
        [FromServices] IMediator mediator,
        [FromQuery] ListAllUsuariosRequest request) => ResponseCode(await mediator.Send(request));

    
}