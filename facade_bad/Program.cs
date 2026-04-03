using System;
using facade_bad.Models;
using facade_bad.Services;
using facade_bad.Infrastructure;

namespace facade_bad;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== User Registration ===");

        var userService = new UserService();
        var emailService = new EmailService();
        var validationService = new ValidationService();
        var passwordHasher = new PasswordHasher();
        var tokenService = new TokenService();
        var repository = new UserRepository();
        var logger = new Logger();

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        // 1. Валидация
        if (!validationService.ValidateEmail(email))
        {
            Console.WriteLine("Invalid email format");
            return;
        }

        if (!validationService.ValidatePassword(password))
        {
            Console.WriteLine("Password too weak");
            return;
        }

        // 2. Проверка уникальности
        if (repository.Exists(email))
        {
            Console.WriteLine("User already exists");
            return;
        }

        // 3. Хеширование
        string hash = passwordHasher.Hash(password);

        // 4. Создание пользователя
        var user = userService.Create(email, hash);

        // 5. Генерация токена
        string token = tokenService.GenerateConfirmationToken();
        user.ConfirmationToken = token;

        // 6. Сохранение
        repository.Save(user);

        // 7. Email
        emailService.SendConfirmationEmail(user.Email, token);

        // 8. Логирование
        logger.Info($"User registered: {user.Email}");

        Console.WriteLine("Registration completed");
    }
}