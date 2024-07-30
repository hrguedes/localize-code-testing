using System.Globalization;

namespace Hrguedes.Localize.Api.Common;

/// <summary>
/// Settings
/// </summary>
public static class Settings
{
    /// <summary>
    /// Token
    /// </summary>
    public static readonly string JwtToken = Environment.GetEnvironmentVariable("JWT_TOKEN") ?? "a5cfad138d2ee057d288081538278d4a";
}