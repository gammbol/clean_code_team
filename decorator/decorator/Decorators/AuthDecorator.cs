// Конкретный декоратор
// проверка авторизации

namespace HttpDecoratorSystem.Decorators
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Models;
    using HttpDecoratorSystem.Services;

    public class AuthDecorator : RequestHandlerDecorator
    {
        // проверяет действителен ли токен
        private readonly ITokenValidator _tokenValidator;

        // обязательна ли авторизация для этого эндпоинта
        private readonly bool _requireAuth;

        public AuthDecorator(IHttpRequestHandler innerHandler,
            ITokenValidator tokenValidator,
            bool requireAuth = true)
            : base(innerHandler)
        {
            _tokenValidator = tokenValidator;
            _requireAuth = requireAuth;
        }

        //  + проверку авторизации
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            //  проверка токена 
            if (_requireAuth)
            {
                if (string.IsNullOrWhiteSpace(request.AuthToken))
                {
                    Console.WriteLine("(дек пути): Токен отсутствует");
                    return HttpResponse.Unauthorized("Требуется авторизация");
                }

                // валидируем токен через сервис
                var validationResult = await _tokenValidator.ValidateAsync(request.AuthToken, ct);

                if (!validationResult.IsValid)
                {
                    Console.WriteLine($"(дек пути): Токен невалиден: {validationResult.ErrorMessage}");
                    return HttpResponse.Unauthorized(validationResult.ErrorMessage);
                }

                // сохраняем данные пользователя в запрос
                request.UserId = validationResult.UserId;
                Console.WriteLine($"(дек пути): Пользователь авторизован: {validationResult.Username}");
            }

            return await _innerHandler.HandleAsync(request, ct);
        }
    }
}
