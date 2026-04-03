// Контекст пользователя
// Хранит информацию об авторизованном пользователе

namespace HttpDecoratorSystem.Models
{
    public class UserContext
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public List<string> Roles { get; set; }

        public bool IsAuthenticated { get; set; }

        public UserContext()
        {
            Roles = new List<string>();  // Инициализируем список ролей
        }

        // есть ли у пользователя определённая роль
        public bool HasRole(string role) => Roles.Contains(role);
    }
}
