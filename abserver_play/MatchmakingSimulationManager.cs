using System;

namespace MatchmakingObserver.subscriber
{
    public class MatchmakingSimulationManager
    {
        private MatchmakingService _matchmakingService = null!;
        private AnalyticsLogger _analyticsLogger = null!;
        private PlayerStateManager _playerStateManager = null!;
        private ReplayRecorder _replayRecorder = null!;

        // запускает всю симуляцию
        public void Run()
        {
            PrintHeader();

            InitializeComponents();
            ConfigureSubscriptions();

            RunSimulationScenarios();

            PrintFinalStatistics();

            PrintFooter();
        }

        // Создание всех компонентов системы.
        private void InitializeComponents()
        {
            Console.WriteLine("\n------------------------");
            Console.WriteLine("ШАГ 1: ИНИЦИАЛИЗАЦИЯ КОМПОНЕНТОВ");
            Console.WriteLine("\n------------------------");

            // Создаём издателя
            _matchmakingService = new MatchmakingService();

            // Создаём подписчиков
            var uiHandler1 = new UINotificationHandler("Player1");
            var uiHandler2 = new UINotificationHandler("Player2");
            var achievementSystem = new AchievementSystem();
            _analyticsLogger = new AnalyticsLogger();
            _playerStateManager = new PlayerStateManager();
            _replayRecorder = new ReplayRecorder();

            Console.WriteLine("\nINIT: Все компоненты созданы успешно");
        }

        // Настройка подписок 
        private void ConfigureSubscriptions()
        {
            Console.WriteLine("\n------------------------");
            Console.WriteLine("ШАГ 2: НАСТРОЙКА ПОДПИСОК (Observer Pattern)");
            Console.WriteLine("\n------------------------");

            // Подписка Player1 на все события
            _matchmakingService.Events.Subscribe("MatchFound", new UINotificationHandler("Player1"));
            _matchmakingService.Events.Subscribe("MatchStarted", new UINotificationHandler("Player1"));
            _matchmakingService.Events.Subscribe("MatchCompleted", new UINotificationHandler("Player1"));

            // Подписка Player2
            _matchmakingService.Events.Subscribe("MatchFound", new UINotificationHandler("Player2"));
            _matchmakingService.Events.Subscribe("MatchStarted", new UINotificationHandler("Player2"));
            _matchmakingService.Events.Subscribe("MatchCompleted", new UINotificationHandler("Player2"));

            // Подписка системных компонентов
            _matchmakingService.Events.Subscribe("MatchFound", new AchievementSystem());
            _matchmakingService.Events.Subscribe("MatchCompleted", new AchievementSystem());

            _matchmakingService.Events.Subscribe("MatchFound", _analyticsLogger);
            _matchmakingService.Events.Subscribe("MatchStarted", _analyticsLogger);
            _matchmakingService.Events.Subscribe("MatchCompleted", _analyticsLogger);

            _matchmakingService.Events.Subscribe("MatchFound", _playerStateManager);
            _matchmakingService.Events.Subscribe("MatchCompleted", _playerStateManager);

            _matchmakingService.Events.Subscribe("MatchStarted", _replayRecorder);
            _matchmakingService.Events.Subscribe("MatchCompleted", _replayRecorder);

            Console.WriteLine("\nCONFIG:  Все подписки настроены");
        }

        // Запуск сценариев симуляции
        private void RunSimulationScenarios()
        {
            Console.WriteLine("\n------------------------");
            Console.WriteLine("ШАГ 3: ЗАПУСК СЦЕНАРИЕВ СИМУЛЯЦИИ");
            Console.WriteLine("\n------------------------");


            // 1: Обычные матчи

            Console.WriteLine("\nСЦЕНАРИЙ 1: Обычные матчи (Ranked & Casual)");
            Console.WriteLine("\n------------------------");

            // Матч #1: Ranked 5v5
            _matchmakingService.FindMatch("Ranked_5v5", ["Player1", "Player2", "Player3", "Player4"]);
            _matchmakingService.StartMatch("M-001", ["Player1", "Player2", "Player3", "Player4"]);
            _matchmakingService.CompleteMatch("M-001", ["Player1", "Player2", "Player3", "Player4"], "Player1");

            System.Threading.Thread.Sleep(1000);

            // Матч #2: Casual 3v3
            _matchmakingService.FindMatch("Casual_3v3", ["Player1", "Player5", "Player6"]);
            _matchmakingService.StartMatch("M-002", ["Player1", "Player5", "Player6"]);
            _matchmakingService.CompleteMatch("M-002", ["Player1", "Player5", "Player6"], "Player5");


            // 2: Демонстрация динамической отписки

            Console.WriteLine("\nСЦЕНАРИЙ 2: Турнирный режим (отключаем аналитику)");
            Console.WriteLine("\n------------------------");
            Console.WriteLine("\nINFO: Отключаем аналитику для снижения нагрузки...");

            _matchmakingService.Events.Unsubscribe("MatchFound", _analyticsLogger);
            _matchmakingService.Events.Unsubscribe("MatchStarted", _analyticsLogger);
            _matchmakingService.Events.Unsubscribe("MatchCompleted", _analyticsLogger);

            // Матч #3: Tournament Final
            _matchmakingService.FindMatch("Tournament_Final", ["Player1", "Player7"]);
            _matchmakingService.StartMatch("M-003", ["Player1", "Player7"]);
            _matchmakingService.CompleteMatch("M-003", ["Player1", "Player7"], "Player7");
        }

        // Вывод финальной статистики
        private void PrintFinalStatistics()
        {
            Console.WriteLine("\n------------------------");
            Console.WriteLine("ШАГ 4: ФИНАЛЬНАЯ СТАТИСТИКА");
            Console.WriteLine("\n------------------------");

            _analyticsLogger.PrintStatistics();
            _playerStateManager.PrintAllStatuses();
            _replayRecorder.PrintSavedReplays();
        }

        // вывод заголовка
        private static void PrintHeader()
        {
            Console.WriteLine("ИГРОВОЙ СЕРВЕР: МАТЧМЕЙКИНГ (Observer Pattern)");
        }

        // вывод подвала
        private static void PrintFooter()
        {
            Console.WriteLine("СИМУЛЯЦИЯ ЗАВЕРШЕНА ");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}