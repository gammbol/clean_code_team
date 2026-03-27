// Клиент (Client) паттерна: игровой уровень.
// Использует только интерфейсы продуктов и фабрики, не зависит от конкретных классов.
// Инкапсулирует логику прохождения одного уровня: отрисовка, бой, сбор ресурсов.

using System;
using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Infrastructure
{
    public class GameLevel
    {
        private readonly IBiomeFactory _factory;
        private readonly Player _player;
        private readonly IEnemy _enemy;
        private readonly IWeapon _weapon;
        private readonly IEnvironmentItem _item;
        private readonly ILevelMap _map;

        public GameLevel(IBiomeFactory factory, Player player)
        {
            _factory = factory;
            _player = player;
            _map = factory.CreateMap();
            _enemy = factory.CreateEnemy();
            _weapon = factory.CreateWeapon();
            _item = factory.CreateItem();
            _player.CurrentWeapon = _weapon;
        }

        public void PlayLevel()
        {
            Console.WriteLine($"\n ЗАГРУЗКА УРОВНЯ: {_factory.BiomeName} ");
            _map.Render();

            float envDamage = _map.CalculateEnvironmentalDamage(_player);
            if (envDamage > 0)
            {
                _player.TakeDamage((int)envDamage);
            }

            Console.WriteLine("\n БОЙ ");
            int turn = 0;
            while (_player.IsAlive && _enemy.Health > 0 && turn < 5)
            {
                turn++;
                Console.WriteLine($"\n[Ход {turn}]");
                _player.CurrentWeapon?.Use(_player, _enemy);
                if (_enemy.Health > 0)
                {
                    _enemy.Attack(_player);
                }
            }

            if (_player.IsAlive)
            {
                Console.WriteLine("\n ИССЛЕДОВАНИЕ ");
                _item.Collect(_player);
            }

            Console.WriteLine($"\n# РЕЗУЛЬТАТ УРОВНЯ #");
            if (_player.IsAlive)
            {
                Console.WriteLine($"Статус: ПОБЕДА");
                Console.WriteLine($"Инвентарь: {string.Join(", ", _player.Inventory)}");
            }
            else
            {
                Console.WriteLine($"Статус: ПОРАЖЕНИЕ (Игрок погиб)");
            }
            Console.WriteLine($"Остаток HP: {_player.Health}");
        }
    }
}