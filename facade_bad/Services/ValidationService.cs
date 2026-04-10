namespace facade_bad.Services;

public class ValidationService
{
    public bool ValidateEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
    }

    public bool ValidatePassword(string password)
    {
        return password.Length >= 6;
    }
}