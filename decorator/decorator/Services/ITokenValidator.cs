// Интерфейс валидатора токенов и его реализация
// эмулируем проверку токенов через бд

namespace HttpDecoratorSystem.Services
{
    public interface ITokenValidator
    {
        Task<TokenValidationResult> ValidateAsync(string token, CancellationToken ct = default);
    }

    // результат валидации токена
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

    // эмуляция сервиса валидации токенов
    public class TokenValidator : ITokenValidator
    {
        private readonly Dictionary<string, TokenValidationResult> _validTokens;

        public TokenValidator()
        {
            _validTokens = new Dictionary<string, TokenValidationResult>
            {
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

        // проверяет токен на валидность
        public async Task<TokenValidationResult> ValidateAsync(string token, CancellationToken ct = default)
        {
            await Task.Delay(50, ct);

            if (_validTokens.TryGetValue(token, out var result))
                return result;

            // не найден
            return new TokenValidationResult
            {
                IsValid = false,
                ErrorMessage = "Невалидный токен"
            };
        }
    }
}
