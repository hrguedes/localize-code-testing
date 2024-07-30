using Asp.Versioning;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Create;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetByClienteId;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetById;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.Update;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hrguedes.Localize.Api.Controllers.v1.Operacao;

/// <summary>
///     Cobrança
/// </summary>
[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/operacao/cobranca")]
public class CobrancaController : BaseController
{
    /// <summary>
    ///     Ctor
    /// </summary>
    /// <param name="logger"></param>
    public CobrancaController(ILogger<BaseController> logger) : base(logger)
    {
    }

    /// <summary>
    ///     Obter por Id
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/operacao/cobranca/1
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
    [ProducesResponseType(typeof(HttpResult<GetCobrancaByIdResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<GetCobrancaByIdResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Get(
        [FromServices] IMediator mediator,
        [FromRoute] int id)
    {
        return ResponseCode(await mediator.Send(new GetCobrancaByIdRequest
        {
            CobrancaId = id
        }));
    }

    /// <summary>
    ///     Cadastrar
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     POST /api/v1/operacao/cobranca
    ///     {
    ///         "pago": true,
    ///         "valor": 10.90,
    ///         "descricao": "Claro Conta de telefone e Internet",
    ///         "dataVencimento": "2024-10-31",
    ///         "clienteId": 1
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
    [ProducesResponseType(typeof(HttpResult<CreateCobrancaResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<CreateCobrancaResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Create(
        [FromServices] IMediator mediator,
        [FromBody] CreateCobrancaRequest request)
    {
        return ResponseCode(await mediator.Send(request));
    }

    /// <summary>
    ///     Atualizar
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     PUT /api/v1/operacao/cobranca
    ///     {
    ///         "id": 2,
    ///         "pago": true,
    ///         "valor": 10.90,
    ///         "descricao": "Claro Conta de telefone e Internet",
    ///         "dataVencimento": "2024-10-31",
    ///         "clienteId": 1
    ///     }
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
    [ProducesResponseType(typeof(HttpResult<UpdateCobrancaResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<UpdateCobrancaResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Post(
        [FromServices] IMediator mediator,
        [FromBody] UpdateCobrancaRequest request)
    {
        return ResponseCode(await mediator.Send(request));
    }

    /// <summary>
    ///     Listar Por Cliente Id
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     GET /api/v1/operacao/cobranca/ClienteId=1&amp;Pagina=1&amp;TotalPorPagina=12
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
    [ProducesResponseType(typeof(HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>), 200)]
    [ProducesResponseType(typeof(HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Listar(
        [FromServices] IMediator mediator,
        [FromQuery] GetCobrancasByClienteIdRequest request)
    {
        return ResponseCode(await mediator.Send(request));
    }
}