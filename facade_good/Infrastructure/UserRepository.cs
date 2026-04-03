using facade_good.Models;

namespace facade_good.Infrastructure;

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