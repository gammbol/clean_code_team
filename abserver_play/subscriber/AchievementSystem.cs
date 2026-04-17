using System;
using System.Collections.Generic;

namespace MatchmakingObserver.subscriber
{
    // выдача достижений игрокам
    public class AchievementSystem : IEventListener
    {
        // Хранилище достижений: Игрок + Список достижений
        private readonly Dictionary<string, HashSet<string>> _playerAchievements;

        public AchievementSystem()
        {
            _playerAchievements = new Dictionary<string, HashSet<string>>();
            Console.WriteLine("AchievementSystem: Система достижений инициализирована");
        }

        public void OnEventOccurred(string eventType, MatchEventData eventData)
        {
            // Реагируем только на завершение матча
            if (eventType != "MatchCompleted")
                return;

            CheckAndAwardAchievements(eventData);
        }

        private void CheckAndAwardAchievements(MatchEventData data)
        {
            string winner = data.AdditionalInfo?.Replace("Winner: ", "") ?? "";

            // Проверяем каждого игрока
            foreach (var player in data.Players)
            {
                // Инициализируем список достижений игрока, если нужно
                if (!_playerAchievements.ContainsKey(player))
                {
                    _playerAchievements[player] = new HashSet<string>();
                }

                // Проверяем достижения
                CheckFirstMatchAchievement(player);

                if (player == winner)
                {
                    CheckFirstWinAchievement(player);
                }
            }
        }

        private void CheckFirstMatchAchievement(string player)
        {
            if (!_playerAchievements[player].Contains("FirstMatch"))
            {
                AwardAchievement(player, "FirstMatch", "Первый матч", "Сыграйте свой первый матч");
            }
        }

        private void CheckFirstWinAchievement(string player)
        {
            if (!_playerAchievements[player].Contains("FirstWin"))
            {
                AwardAchievement(player, "FirstWin", "Первая победа", "Выиграйте свой первый матч");
            }
        }

        private void AwardAchievement(string player, string achievementId, string title, string description)
        {
            _playerAchievements[player].Add(achievementId);

            Console.WriteLine($"Achievement:    {player} получил достижение:");
            Console.WriteLine($"Achievement:    {title} - {description}");
        }
    }
}