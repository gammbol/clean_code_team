// Конкретный декоратор
// Добавляет функциональность ПРОВЕРКИ АВТОРИЗАЦИИ.
// Если токен невалиден — возвращает 401 Unauthorized и не вызывает следующие обработчики.

namespace HttpDecoratorSystem.Decorators
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Models;
    using HttpDecoratorSystem.Services;

    public class AuthDecorator : RequestHandlerDecorator
    {
        // Сервис валидации токенов (проверяет, действителен ли токен)
        private readonly ITokenValidator _tokenValidator;

        // Флаг: обязательна ли авторизация для этого эндпоинта
        private readonly bool _requireAuth;

        // Конструктор: принимает обработчик, валидатор и флаг обязательности
        public AuthDecorator(IHttpRequestHandler innerHandler,
            ITokenValidator tokenValidator,
            bool requireAuth = true)
            : base(innerHandler)
        {
            _tokenValidator = tokenValidator;
            _requireAuth = requireAuth;
        }

        // Переопределяем метод обработки, добавляя проверку авторизации
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            //  Проверка токена 
            if (_requireAuth)
            {
                // Проверяем: есть ли токен в запросе
                if (string.IsNullOrWhiteSpace(request.AuthToken))
                {
                    Console.WriteLine("[AUTH]Токен отсутствует");
                    // Возвращаем ошибку 401 и НЕ вызываем следующие обработчики
                    return HttpResponse.Unauthorized("Требуется авторизация");
                }

                // Валидируем токен через сервис
                var validationResult = await _tokenValidator.ValidateAsync(request.AuthToken, ct);

                // Если токен невалиден — возвращаем ошибку
                if (!validationResult.IsValid)
                {
                    Console.WriteLine($"[AUTH]Токен невалиден: {validationResult.ErrorMessage}");
                    return HttpResponse.Unauthorized(validationResult.ErrorMessage);
                }

                // Сохраняем данные пользователя в запрос для следующих обработчиков
                // Это нужно, чтобы базовый обработчик знал, кто сделал запрос
                request.UserId = validationResult.UserId;
                Console.WriteLine($"[AUTH] Пользователь авторизован: {validationResult.Username}");
            }

            // Если авторизация успешна — передаём запрос дальше по цепочке
            return await _innerHandler.HandleAsync(request, ct);
        }
    }
}