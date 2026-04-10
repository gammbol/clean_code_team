namespace facade_bad.Services;

public class TokenService
{
    public string GenerateConfirmationToken()
    {
        return Guid.NewGuid().ToString();
    }
}