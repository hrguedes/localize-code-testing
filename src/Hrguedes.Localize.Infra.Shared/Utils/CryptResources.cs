namespace Hrguedes.Localize.Infra.Shared.Utils;


public static class CryptResources
{
    private static readonly Random Random = new Random();

    public static string GenerateSecurePassword(int length = 12)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
        return new string(Enumerable.Repeat(validChars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}