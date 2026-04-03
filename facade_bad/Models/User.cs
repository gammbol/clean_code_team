namespace facade_bad.Models;

public class User
{
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string ConfirmationToken { get; set; } = "";
    public bool IsConfirmed { get; set; }
}