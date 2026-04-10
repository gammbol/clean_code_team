using facade_bad.Models;

namespace facade_bad.Infrastructure;

public class UserRepository
{
    private static readonly List<User> _users = new();

    public void Save(User user)
    {
        _users.Add(user);
    }

    public bool Exists(string email)
    {
        return _users.Any(u => u.Email == email);
    }
}