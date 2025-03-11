public class JwtSecretKeyService
{
    public void GenerateAndDisplayKey(int length = 32)
    {
        try
        {
            var secretKey = JwtSecretKeyGenerator.GenerateRandomKey(length);
            Console.WriteLine($"SecretKey: {secretKey}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
