namespace Restaurant.Interfaces;

public interface IHashServices
{
    string CreateHashedPassword(string password);
    bool VerifyPassword(string passedPassword, string dbPassword);
}
