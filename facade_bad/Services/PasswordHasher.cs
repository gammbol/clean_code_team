namespace facade_bad.Services;

public class PasswordHasher
{
    public string Hash(string password)
    {
        return $"HASHED_{password}";
    }
}