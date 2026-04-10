namespace facade_good.Services;

public class PasswordHasher
{
    public string Hash(string password)
    {
        return $"HASHED_{password}";
    }
}