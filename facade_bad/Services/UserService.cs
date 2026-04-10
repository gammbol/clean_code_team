using facade_bad.Models;

namespace facade_bad.Services;

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