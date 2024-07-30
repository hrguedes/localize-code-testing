namespace Hrguedes.Localize.Infra.Shared.Models;

/// <summary>
/// Modelo de validação de propieadedes (Utilizados para retornar bad request com a propiedade validada e a mensagem de validação)
/// </summary>
/// <param name="key">Nome da propiedade</param>
/// <param name="message">Mensagem de validação</param>
public sealed class ValidationModel(string key, string message)
{
    /// <summary>
    /// Propieade validada
    /// </summary>
    public string Key { get; set; } = key;

    /// <summary>
    /// Mensagem de validação
    /// </summary>
    public string Message { get; set; } = message;
}
