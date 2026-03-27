// Контекст пользователя
// Хранит информацию об авторизованном пользователе

namespace HttpDecoratorSystem.Models
{
    public class UserContext
    {
        // Уникальный ID пользователя в системе
        public string UserId { get; set; }

        // Отображаемое имя пользователя
        public string Username { get; set; }

        // Роли пользователя (например: "user", "admin", "moderator")
        public List<string> Roles { get; set; }

        // Флаг: авторизован ли пользователь
        public bool IsAuthenticated { get; set; }

        // Конструктор
        public UserContext()
        {
            Roles = new List<string>();  // Инициализируем список ролей
        }

        // Проверка: есть ли у пользователя определённая роль
        public bool HasRole(string role) => Roles.Contains(role);
    }
}