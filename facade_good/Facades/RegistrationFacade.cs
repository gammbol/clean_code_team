using facade_good.Models;
using facade_good.Services;
using facade_good.Infrastructure;

namespace facade_good.Facades;

public class RegistrationFacade
{
    private readonly UserService _userService;
    private readonly EmailService _emailService;
    private readonly ValidationService _validationService;
    private readonly PasswordHasher _passwordHasher;
    private readonly TokenService _tokenService;
    private readonly UserRepository _repository;
    private readonly Logger _logger;

    public RegistrationFacade()
    {
        _userService = new UserService();
        _emailService = new EmailService();
        _validationService = new ValidationService();
        _passwordHasher = new PasswordHasher();
        _tokenService = new TokenService();
        _repository = new UserRepository();
        _logger = new Logger();
    }

    public bool Register(string email, string password)
    {
        // 1. Валидация
        if (!_validationService.ValidateEmail(email))
        {
            Console.WriteLine("Invalid email format");
            return false;
        }

        if (!_validationService.ValidatePassword(password))
        {
            Console.WriteLine("Password too weak");
            return false;
        }

        // 2. Проверка уникальности
        if (_repository.Exists(email))
        {
            Console.WriteLine("User already exists");
            return false;
        }

        // 3. Хеширование
        string hash = _passwordHasher.Hash(password);

        // 4. Создание пользователя
        var user = _userService.Create(email, hash);

        // 5. Генерация токена
        string token = _tokenService.GenerateConfirmationToken();
        user.ConfirmationToken = token;

        // 6. Сохранение
        _repository.Save(user);

        // 7. Email
        _emailService.SendConfirmationEmail(user.Email, token);

        // 8. Логирование
        _logger.Info($"User registered: {user.Email}");

        return true;
    }
}