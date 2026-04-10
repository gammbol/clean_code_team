using facade_good.Models;

namespace facade_good.Services;

public class UserService
{
    public User Create(string email, string passwordHash)
    {
        return new User
        {
            Email = email,
            PasswordHash = passwordHash,
            IsConfirmed = false
        };
    }
}