using Asp.Versioning;
using Hrguedes.Localize.Api.Common;
using Hrguedes.Localize.Api.Common.Models;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Infra.Shared.Http;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hrguedes.Localize.Api.Controllers.v1;


/// <summary>
/// Autenticação e Autorização
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/authentication")]
public class AuthenticationController : BaseController
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public AuthenticationController(ILogger<BaseController> logger) : base(logger)
    {
    }
    
    /// <summary>
    /// Autenticar usuário
    /// </summary>
    /// <remarks>
    /// Exemplo:
    /// 
    ///     POST /api/v1/Authentication/login
    ///     {
    ///        "email": "joao.mendes@example.com",
    ///        "senha": "Senha123"
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
    [ProducesResponseType(typeof(HttpResult<string>), 200)]
    [ProducesResponseType(typeof(HttpResult<string>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Post(
        [FromServices] IMediator mediator,
        [FromBody] AuthenticateUsuarioRequest request)
    {
        try
        {
            var response = await mediator.Send(new GetUsuarioByEmailRequest(request.Email));
            if (!response.Ok)
                return ResponseCode(response);

            var token = await TokenService.GenerateToken(response.Value!);
            return ResponseCode(!string.IsNullOrEmpty(token) 
                ? HttpResult<string>.Success(token, "Ok") 
                : HttpResult<string>.BadRequest("Ocorreu um erro ao gerar o Token"));
        }
        catch (Exception e)
        {
            return ResponseCode(HttpResult<string>.InternalServerError(e.Message));
        }
    }
}