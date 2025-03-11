using System.Security.Cryptography;

public class JwtSecretKeyGenerator
{
    /// <summary>
    /// Generates a random secret key using a cryptographically secure random number generator.
    /// </summary>
    /// <param name="length">The length of the key in bytes (default is 32 bytes).</param>
    /// <returns>A base64-encoded random key.</returns>
    /// <exception cref="ArgumentException">Thrown if the length is less than or equal to 0.</exception>
    public static string GenerateRandomKey(int length = 32)
    {
        if (length <= 0)
        {
            throw new ArgumentException("Length must be a positive integer.", nameof(length));
        }

        var randomBytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }
}
