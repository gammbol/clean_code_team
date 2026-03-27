using System;

namespace method_test.Models
{
    // Модель данных: результат выполнения теста.
    // Содержит статус прохождения, сообщение, время выполнения и возможное исключение.
    // Не содержит бизнес-логики, только свойства.
    public class TestResult
    {
        public bool IsPassed { get; set; }
        public string Message { get; set; }
        public long DurationMs { get; set; }
        public Exception Error { get; set; }
    }
}