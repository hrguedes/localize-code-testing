using System.Net;
using System.Security.Claims;
using Hrguedes.Localize.Infra.Shared.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrguedes.Localize.Api.Controllers;


/// <summary>
///     Abstracão do (ControllerBase)
/// </summary>
public class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get id user logged 
    /// </summary>
    /// <returns></returns>
    protected Guid GetUserId() => Guid.Parse(HttpContext.User.Claims.FirstOrDefault(cond => cond.Type == ClaimTypes.NameIdentifier)!.Value);

    /// <summary>
    ///     Default Response
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected ObjectResult ResponseCode<T>(HttpResult<T> result)
    {
        try
        {
            return StatusCode((int)result.HttpStatusCode, result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            result.HttpStatusCode = HttpStatusCode.InternalServerError;
            result.Message = "Ocorreu um erro interno, por favor contactar o administrador.";
            result.Ok = false;
            return StatusCode(500, result);
        }
    }
}