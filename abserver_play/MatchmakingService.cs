using System;

namespace MatchmakingObserver
{
    // издатель - содержит бизнес-логику матчмейкинга
    // публикует события через EventManager
    public class MatchmakingService
    {
        // EventManager-у делегируем всю работу с подписчиками
        public EventManager Events { get; private set; }

        private int _matchCounter = 0;

        public MatchmakingService()
        {
            Events = new EventManager();
            Console.WriteLine("MatchmakingService:  Сервис матчмейкинга инициализирован\n");
        }

        // поиск матча для игроков..
        public void FindMatch(string queueId, string[] players)
        {
            Console.WriteLine("\n--------------");
            Console.WriteLine($"Matchmaking:  Начало поиска матча в очереди {queueId}");
            Console.WriteLine($"Matchmaking:  Игроки: {string.Join(", ", players)}");

            // производим поиск по игрокам
            SimulateMatchmakingProcess(players);

            // Генерируем уникальный ID матча
            _matchCounter++;
            string matchId = $"M-{_matchCounter:D3}";

            // Создаём данные события
            var eventData = new MatchEventData(
                eventType: "MatchFound",
                matchId: matchId,
                players: players,
                additionalInfo: $"Queue: {queueId}"
            );

            // оповещаем всех подписчиков
            Events.Notify("MatchFound", eventData);

            Console.WriteLine($"Matchmaking:  Матч {matchId} успешно сформирован");
        }

        // начало матча.
        public void StartMatch(string matchId, string[] players)
        {
            Console.WriteLine($"\nMatchmaking:  Матч {matchId} начинается!");

            var eventData = new MatchEventData(
                eventType: "MatchStarted",
                matchId: matchId,
                players: players,
                additionalInfo: "Match in progress"
            );

            Events.Notify("MatchStarted", eventData);
        }

        // завершение матча.

        public void CompleteMatch(string matchId, string[] players, string winner)
        {
            Console.WriteLine($"\nMatchmaking:  Матч {matchId} завершён!");
            Console.WriteLine($"Matchmaking:  Победитель: {winner}");

            var eventData = new MatchEventData(
                eventType: "MatchCompleted",
                matchId: matchId,
                players: players,
                additionalInfo: $"Winner: {winner}"
            );

            Events.Notify("MatchCompleted", eventData);
        }

        // имитация процесса поиска матча
        private void SimulateMatchmakingProcess(string[] players)
        {
            Console.WriteLine("Matchmaking:  Анализ параметров игроков...");
            System.Threading.Thread.Sleep(500); 

            Console.WriteLine("Matchmaking:  Поиск подходящих оппонентов...");
            System.Threading.Thread.Sleep(500);

            Console.WriteLine("Matchmaking:  Формирование сбалансированных команд...");
            System.Threading.Thread.Sleep(500);
        }
    }
}