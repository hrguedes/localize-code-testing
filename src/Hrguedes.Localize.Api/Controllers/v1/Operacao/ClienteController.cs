using Asp.Versioning;
using Hrguedes.Localize.Application.Features.Clientes.Commands.Create;
using Hrguedes.Localize.Application.Features.Clientes.Commands.GetById;
using Hrguedes.Localize.Application.Features.Clientes.Commands.ListAll;
using Hrguedes.Localize.Application.Features.Clientes.Commands.Update;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrguedes.Localize.Api.Controllers.v1.Operacao;

/// <summary>
///     Clientes
/// </summary>
[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/operacao/clientes")]
public class ClienteController : BaseController
{
    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="logger"></param>
    public ClienteController(ILogger<BaseController> logger) : base(logger)
    {
    }

    /// <summary>
    ///     Obter por Id
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/operacao/clientes/1
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
    [ProducesResponseType(typeof(HttpResult<GetClienteByIdResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<GetClienteByIdResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator mediator,
        [FromRoute] int id)
    {
        return ResponseCode(await mediator.Send(new GetClienteByIdRequest
        {
            ClienteId = id,
            UsuarioId = GetUserId()
        }));
    }

    /// <summary>
    ///     Cadastrar
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     POST /api/v1/operacao/clientes
    ///     {
    ///         "nome": "Jose da Colina",
    ///         "documento": "41231299812",
    ///         "telefone": "5514998223212",
    ///         "endereco": "Rua dos pinhais 2350, Bauru, São Paulo"
    ///     }
    /// 
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns> {} </returns>
    /// <response code="200">{}</response>
    /// <response code="400">Erro de Cliente</response>
    /// <response code="500">Erro de Servidor</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(HttpResult<CreateClienteResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<CreateClienteResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Create(
        [FromServices] IMediator mediator,
        [FromBody] CreateClienteRequest request)
    {
        request.UsuarioId = GetUserId();
        return ResponseCode(await mediator.Send(request));
    }

    /// <summary>
    ///     Atualizar
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     PUT /api/v1/operacao/clientes
    ///     {
    ///         "id": 12,
    ///         "nome": "Jose da Colina",
    ///         "documento": "41231299812",
    ///         "telefone": "5514998223212",
    ///         "endereco": "Rua dos pinhais 2350, Bauru, São Paulo"
    ///     }
    ///
    /// 
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns> {} </returns>
    /// <response code="200">{}</response>
    /// <response code="400">Erro de Cliente</response>
    /// <response code="500">Erro de Servidor</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(typeof(HttpResult<UpdateClienteResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<UpdateClienteResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Post(
        [FromServices] IMediator mediator,
        [FromBody] UpdateClienteRequest request)
    {
        request.UsuarioId = GetUserId();
        return ResponseCode(await mediator.Send(request));
    }

    /// <summary>
    ///     Listar Clientes
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/operacao/clientes?Pesquisa=sd&amp;Pagina=1&amp;TotalPorPagina=12
    /// 
    /// </remarks>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns> [{}] </returns>
    /// <response code="200">[{}]</response>
    /// <response code="400">Erro de Cliente</response>
    /// <response code="500">Erro de Servidor</response>
    [HttpGet("all")]
    [Authorize]
    [ProducesResponseType(typeof(HttpResult<PaginationResponse<ListAllClientesResponse>>), 200)]
    [ProducesResponseType(typeof(HttpResult<PaginationResponse<ListAllClientesResponse>>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Listar(
        [FromServices] IMediator mediator,
        [FromQuery] ListAllClientesRequest request)
    {
        request.UsuarioId = GetUserId();
        return ResponseCode(await mediator.Send(request));
    }
}