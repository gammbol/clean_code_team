// Инкапсулирует инициализацию игры и запуск кампании.
// для разгружения Main - а клиент только создает зависимости и делегирует выполнение

using System;
using afabric_game.Models;

namespace afabric_game.Infrastructure
{
    public class GameRunner
    {
        private readonly Player _player;

        public GameRunner(Player player)
        {
            _player = player;
        }

        public void Start()
        {
            var game = new GameManager(_player);
            game.RunCampaign();
        }

        public static Player CreateDefaultPlayer(string name)
        {
            return new Player { Name = name };
        }
    }
}