using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hrguedes.Localize.Application.Features.Usuarios.Models;
using Microsoft.IdentityModel.Tokens;

namespace Hrguedes.Localize.Api.Common;

/// <summary>
/// Gera o Token JWT (Bearer) junto com as autorizações
/// </summary>
public static class TokenService
{
    /// <summary>
    /// Generate Token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static Task<string> GenerateToken(UsuarioModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.JwtToken);
        var claims = new List<Claim>();
        claims.AddRange(new []
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Email, user.Email)
        });
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}