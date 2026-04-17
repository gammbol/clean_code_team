using System;
using System.Collections.Generic;

namespace MatchmakingObserver.subscriber
{
    // логирование статистики для аналитики
    public class AnalyticsLogger : IEventListener
    {
        private readonly Dictionary<string, int> _eventCounts;

        private int _totalMatches;

        public AnalyticsLogger()
        {
            _eventCounts = new Dictionary<string, int>();
            Console.WriteLine("AnalyticsLogger: Система аналитики инициализирована");
        }

        public void OnEventOccurred(string eventType, MatchEventData eventData)
        {
            // Увеличиваем счётчик события
            if (!_eventCounts.ContainsKey(eventType))
            {
                _eventCounts[eventType] = 0;
            }
            _eventCounts[eventType]++;

            // Логируем в зависимости от типа
            switch (eventType)
            {
                case "MatchFound":
                    LogMatchCreated(eventData);
                    break;

                case "MatchStarted":
                    LogMatchStarted(eventData);
                    break;

                case "MatchCompleted":
                    LogMatchCompleted(eventData);
                    break;
            }
        }

        private void LogMatchCreated(MatchEventData data)
        {
            Console.WriteLine($"Analytics: MatchCreated | ID: {data.MatchId} | " + $"Players: {data.Players.Length} | Time: {data.Timestamp:HH:mm:ss}");
        }

        private void LogMatchStarted(MatchEventData data)
        {
            Console.WriteLine($"Analytics: MatchStarted | ID: {data.MatchId} | " + $"Time: {data.Timestamp:HH:mm:ss}");
        }

        private void LogMatchCompleted(MatchEventData data)
        {
            _totalMatches++;
            string winner = data.AdditionalInfo?.Replace("Winner: ", "") ?? "Unknown";

            Console.WriteLine($"Analytics: MatchCompleted | ID: {data.MatchId} | " + $"Winner: {winner} | Total: {_totalMatches}");
        }

        // Публичный метод для получения статистики
        public void PrintStatistics()
        {
            Console.WriteLine("------------------\n");
            Console.WriteLine("Analytics: СТАТИСТИКА:");
            foreach (var kvp in _eventCounts)
            {
                Console.WriteLine($"Analytics:    {kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine($"Analytics:    Всего матчей: {_totalMatches}");
            Console.WriteLine("------------------\n");
        }
    }
}