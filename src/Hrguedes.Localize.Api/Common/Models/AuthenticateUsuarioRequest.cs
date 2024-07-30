namespace Hrguedes.Localize.Api.Common.Models;

/// <summary>
/// Autenticar usuário
/// </summary>
/// <param name="Email"></param>
/// <param name="Senha"></param>
public record AuthenticateUsuarioRequest(string Email, string Senha);