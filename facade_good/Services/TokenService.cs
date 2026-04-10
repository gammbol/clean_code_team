namespace facade_good.Services;

public class TokenService
{
    public string GenerateConfirmationToken()
    {
        return Guid.NewGuid().ToString();
    }
}