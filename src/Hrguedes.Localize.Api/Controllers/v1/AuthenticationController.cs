using Asp.Versioning;
using Hrguedes.Localize.Api.Common;
using Hrguedes.Localize.Api.Common.Models;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
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
    [ProducesResponseType(typeof(HttpResult<AuthenticateUsuarioResponse>), 200)]
    [ProducesResponseType(typeof(HttpResult<AuthenticateUsuarioResponse>), 400)]
    [ProducesResponseType(typeof(HttpResult<>), 500)]
    public async Task<IActionResult> Post(
        [FromServices] IMediator mediator,
        [FromBody] AuthenticateUsuarioRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Senha))
                return ResponseCode(HttpResult<AuthenticateUsuarioResponse>.BadRequest(nameof(request.Senha), DefaultResources.NaoPodeSerVazio));

            var user = await mediator.Send(new GetUsuarioByEmailRequest(request.Email));
            if (!user.Ok)
                return ResponseCode(user);
            var token = await TokenService.GenerateToken(user.Value!);
            var response = new AuthenticateUsuarioResponse(user.Value!.Nome, user.Value.Email, token, user.Value.Id);
            return ResponseCode(!string.IsNullOrEmpty(token)
                ? HttpResult<AuthenticateUsuarioResponse>.Success(response, "Ok")
                : HttpResult<AuthenticateUsuarioResponse>.BadRequest("Ocorreu um erro ao gerar o Token"));
        }
        catch (Exception e)
        {
            return ResponseCode(HttpResult<AuthenticateUsuarioResponse>.InternalServerError(e.Message));
        }
    }
}