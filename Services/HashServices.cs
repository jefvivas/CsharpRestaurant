using Restaurant.Interfaces;

namespace Restaurant.Services;

public class HashServices : IHashServices
{
    public string CreateHashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public bool VerifyPassword(string passedPassword, string dbPassword)
    {
        return BCrypt.Net.BCrypt.Verify(passedPassword, dbPassword);
    }
}
