namespace Hrguedes.Localize.Api.Common.Models;

/// <summary>
/// Autenticar usuário
/// </summary>
/// <param name="Nome"></param>
/// <param name="Email"></param>
/// <param name="Token"></param>
/// <param name="Id"></param>
public record AuthenticateUsuarioResponse(string Nome, string Email, string Token, Guid Id);