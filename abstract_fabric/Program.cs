// Клиент (Client)
// Отвечает только за конфигурацию - создание игрока и запуск через GameRunner.

using System;
using afabric_game.Infrastructure;

namespace afabric_game
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = GameRunner.CreateDefaultPlayer("Герой");
            var runner = new GameRunner(player);
            runner.Start();
        }
    }
}