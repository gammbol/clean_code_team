using System;
using System.Collections.Generic;

namespace MatchmakingObserver.subscriber
{
    //  управление статусами игроков (Online, InQueue, InGame).
    public class PlayerStateManager : IEventListener
    {
        // Возможные статусы игрока
        public enum PlayerStatus
        {
            Online,      // Игрок в сети
            InQueue,     // Игрок в очереди поиска
            InGame,      // Игрок в матче
            Offline      // Игрок не в сети
        }

        private readonly Dictionary<string, PlayerStatus> _playerStatuses;

        public PlayerStateManager()
        {
            _playerStatuses = new Dictionary<string, PlayerStatus>();
            Console.WriteLine("PlayerStateManager: Менеджер состояний инициализирован");
        }

        public void OnEventOccurred(string eventType, MatchEventData eventData)
        {
            switch (eventType)
            {
                case "MatchFound":
                    SetPlayersStatus(eventData.Players, PlayerStatus.InGame);
                    break;

                case "MatchCompleted":
                    SetPlayersStatus(eventData.Players, PlayerStatus.Online);
                    break;
            }
        }

        private void SetPlayersStatus(string[] players, PlayerStatus status)
        {
            foreach (var player in players)
            {
                _playerStatuses[player] = status;
            }

            string statusName = Enum.GetName(typeof(PlayerStatus), status);
            Console.WriteLine($"PlayerState: Изменён статус на {statusName}: " + $"{string.Join(", ", players)}");
        }


        // метод для проверки статуса игрока
        public PlayerStatus GetPlayerStatus(string playerName)
        {
            return _playerStatuses.TryGetValue(playerName, out var status)
                ? status
                : PlayerStatus.Offline;
        }


        // Вывод всех статусов
        public void PrintAllStatuses()
        {
            Console.WriteLine("\nPlayerStateManager: Текущие статусы:");
            foreach (var kvp in _playerStatuses)
            {
                Console.WriteLine($"PlayerStateManager:    {kvp.Key}: {kvp.Value}");
            }
        }
    }
}