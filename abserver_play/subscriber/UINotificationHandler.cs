using System;

namespace MatchmakingObserver.subscriber
{
    // отображение уведомлений игрокам в интерфейсе

    public class UINotificationHandler : IEventListener
    {
        private readonly string _playerName;

        public UINotificationHandler(string playerName)
        {
            _playerName = playerName;
        }


        public void OnEventOccurred(string eventType, MatchEventData eventData)
        {
            // Показываем уведомления только если игрок участвует в матче
            if (!Array.Exists(eventData.Players, p => p == _playerName))
                return;

            // Реагируем на разные типы событий
            switch (eventType)
            {
                case "MatchFound":
                    ShowMatchFoundNotification(eventData);
                    break;

                case "MatchStarted":
                    ShowMatchStartedNotification(eventData);
                    break;

                case "MatchCompleted":
                    ShowMatchCompletedNotification(eventData);
                    break;
            }
        }

        private void ShowMatchFoundNotification(MatchEventData data)
        {
            Console.WriteLine($"UI:   {_playerName}: Найдено! Матч {data.MatchId}");
            Console.WriteLine($"UI:   Команда: [{string.Join(", ", data.Players)}]");
        }

        private void ShowMatchStartedNotification(MatchEventData data)
        {
            Console.WriteLine($"UI:   {_playerName}: Матч {data.MatchId} начался! В бой!");
        }

        private void ShowMatchCompletedNotification(MatchEventData data)
        {
            string winner = data.AdditionalInfo?.Replace("Winner: ", "") ?? "Неизвестно";
            bool isWinner = winner == _playerName;

            string emoji = isWinner ? "+" : "-";
            string message = isWinner ? "Победа!" : "Поражение...";

            Console.WriteLine($"UI:  {emoji} {_playerName}: {message} ({data.MatchId})");
        }
    }
}