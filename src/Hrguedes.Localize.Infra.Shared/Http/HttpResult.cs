using System.Net;
using FluentValidation.Results;
using Hrguedes.Localize.Infra.Shared.Models;
using Hrguedes.Localize.Infra.Shared.Resources;

namespace Hrguedes.Localize.Infra.Shared.Http;


/// <summary>
/// Classe com padrão de retorno para os endpoins, utilizar como padrão
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class HttpResult<T>
{
    /// <summary>
    /// Objeto de retorno
    /// </summary>
    public T? Value { get; set; }
    /// <summary>
    /// HTTP Code de retorno (200, 400, 500)
    /// https://developer.mozilla.org/en-US/docs/Web/HTTP
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
    /// <summary>
    /// Mensagem padrão de retorno
    /// </summary>
    public string? Message { get; set; } = default!;
    /// <summary>
    /// Mensagem de erros, utilizar para retornar validações de propieades
    /// </summary>
    public ValidationModel[]? ErrorMessages { get; set; } = default!;
    /// <summary>
    /// Em Caso de exeção utilizar o campo Exception
    /// </summary>
    public string? Exception { get; set; } = default!;
    /// <summary>
    /// True requisição realizar com sucesso False para erros de validações ou Exeções
    /// </summary>
    public bool Ok { get; set; } = true;

    /// <summary>
    /// Requisição realizar com sucesso (200, 201 ...)
    /// </summary>
    /// <param name="value">Instancia do objeto de retorno</param>
    /// <param name="message">Mensagem de retorno (Opcional)</param>
    /// <returns></returns>
    public static HttpResult<T> Success(T value, string? message = null)
    {
        return new HttpResult<T>
        {
            Value = value,
            HttpStatusCode = HttpStatusCode.OK,
            Message = message
        };
    }

    /// <summary>
    /// Usuário não foi autenticado
    /// </summary>
    /// <returns></returns>
    public static HttpResult<T> NotAuthenticated()
    {
        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.Unauthorized,
            Message = DefaultResources.UsuarioNaoAutenticado,
            ErrorMessages = null,
            Exception = null,
            Ok = false,
            Value = default!
        };
    }
    
    /// <summary>
    /// O Acesso ao recurso não foi autorizado
    /// </summary>
    /// <returns></returns>
    public static HttpResult<T> NotAuthorized()
    {
        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.Forbidden,
            Message = DefaultResources.RecursoNaoAutorizado,
            ErrorMessages = null,
            Exception = null,
            Ok = false,
            Value = default!
        };
    }
    
    /// <summary>
    /// Requisição com erros de validação (400, 401 ...)
    /// </summary>
    /// <param name="message">Mensagem de Erro</param>
    /// <returns></returns>
    public static HttpResult<T> BadRequest(string message)
    {
        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.BadRequest,
            Message = message,
            ErrorMessages = null,
            Exception = null,
            Ok = false,
            Value = default!
        };
    }

    /// <summary>
    /// Requisição com erros de validação de propiedade (400, 401 ...)
    /// </summary>
    /// <param name="key">Nome da propiedade</param>
    /// <param name="message">Mensagem de validação</param>
    /// <returns></returns>
    public static HttpResult<T> BadRequest(string key, string message)
    {
        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.BadRequest,
            ErrorMessages = [new ValidationModel(key, message)],
            Message = message,
            Exception = null,
            Ok = false,
            Value = default!

        };
    }


    /// <summary>
    /// Requisição com erros de validação de propiedade (400, 401 ...)
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    /// <param name="key">Nome da propiedade</param>
    /// <param name="validationMessage">Mensagem de validação do campo</param>
    /// <returns></returns>
    public static HttpResult<T> BadRequest(string message, string key, string validationMessage)
    {
        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.BadRequest,
            ErrorMessages = [new ValidationModel(key, validationMessage)],
            Message = message,
            Exception = null,
            Ok = false,
            Value = default!

        };
    }

    /// <summary>
    /// Requisição com erros de validação de propiedade (400, 401 ...) (Fluent Validation)
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    /// <param name="validation">Objeto de Validation result do Fluent Validation</param>
    /// <returns></returns>
    public static HttpResult<T> BadRequest(string message, ValidationResult validation)
    {
        var validations = new List<ValidationModel>();
        validation.Errors.ForEach(x => validations.Add(new ValidationModel(x.PropertyName, x.ErrorMessage)));

        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.BadRequest,
            ErrorMessages = validations.ToArray(),
            Message = DefaultResources.ErroValidacao,
            Exception = null,
            Ok = false,
            Value = default!

        };
    }

    /// <summary>
    /// Erro de processamento interno (500 ...) utilizar em Try Catchs
    /// </summary>
    /// <param name="exception">Mensagem da exceptions</param>
    /// <returns></returns>
    public static HttpResult<T> InternalServerError(string exception)
    {
        return new HttpResult<T>
        {
            HttpStatusCode = HttpStatusCode.InternalServerError,
            Exception = exception,
            Message = DefaultResources.ErroInterno,
            Ok = false,
            Value = default!,
            ErrorMessages = null
        };
    }
}
