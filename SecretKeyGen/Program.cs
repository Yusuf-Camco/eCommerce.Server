class Program
{
    static void Main(string[] args)
    {
        var jwtService = new JwtSecretKeyService();
        jwtService.GenerateAndDisplayKey(); // Default length (32 bytes)
        jwtService.GenerateAndDisplayKey(64); // Custom length (64 bytes)
    }
}
