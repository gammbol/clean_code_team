// Интерфейс валидатора токенов и его реализация
// Эмулирует проверку токенов через базу данных или внешний сервис

namespace HttpDecoratorSystem.Services
{
    public interface ITokenValidator
    {
        Task<TokenValidationResult> ValidateAsync(string token, CancellationToken ct = default);
    }

    // Результат валидации токена
    public class TokenValidationResult
    {
        public bool IsValid { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public string ErrorMessage { get; set; }

        public TokenValidationResult()
        {
            Roles = new List<string>();
        }
    }

    // Эмуляция сервиса валидации токенов
    // В реальном проекте здесь был бы запрос к Auth-сервису или БД
    public class TokenValidator : ITokenValidator
    {
        // Словарь валидных токенов (эмуляция базы данных)
        private readonly Dictionary<string, TokenValidationResult> _validTokens;

        public TokenValidator()
        {
            _validTokens = new Dictionary<string, TokenValidationResult>
            {
                // Тестовые токены для демонстрации
                ["valid_token_123"] = new TokenValidationResult
                {
                    IsValid = true,
                    UserId = "user_42",
                    Username = "john_doe",
                    Roles = new List<string> { "user", "admin" }
                },
                ["user_token_456"] = new TokenValidationResult
                {
                    IsValid = true,
                    UserId = "user_99",
                    Username = "jane_smith",
                    Roles = new List<string> { "user" }
                }
            };
        }

        // Проверяет токен на валидность
        public async Task<TokenValidationResult> ValidateAsync(string token, CancellationToken ct = default)
        {
            // Эмуляция задержки сети/БД (50мс)
            await Task.Delay(50, ct);

            // Ищем токен в "базе данных"
            if (_validTokens.TryGetValue(token, out var result))
                return result;

            // Токен не найден
            return new TokenValidationResult
            {
                IsValid = false,
                ErrorMessage = "Невалидный токен"
            };
        }
    }
}