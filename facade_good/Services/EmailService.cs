namespace facade_good.Services;

public class EmailService
{
    public void SendConfirmationEmail(string email, string token)
    {
        Console.WriteLine($"Sending confirmation email to {email}");
        Console.WriteLine($"Confirmation token: {token}");
    }
}