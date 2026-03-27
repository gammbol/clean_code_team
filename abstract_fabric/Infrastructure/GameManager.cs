// Координатор игры: управляет списком доступных фабрик и последовательностью уровней.
// Работает только с интерфейсом IBiomeFactory, не знает о конкретных реализациях.
// Отвечает за итерацию по биомам и проверку условия окончания кампании.

using System;
using System.Collections.Generic;
using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Infrastructure
{
    public class GameManager
    {
        private readonly Player _player;
        private readonly List<IBiomeFactory> _availableBiomes;

        public GameManager(Player player)
        {
            _player = player;
            _availableBiomes = new List<IBiomeFactory>
            {
                new Factories.ForestBiomeFactory(),
                new Factories.DesertBiomeFactory(),
                new Factories.IceBiomeFactory()
            };
        }

        public void RunCampaign()
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Симулятор игрового движка для нескольких биомов");
            Console.WriteLine("------------------------------------------------\n");

            Console.WriteLine($"Игрок: {_player.Name}");
            Console.WriteLine($"Доступно биомов: {_availableBiomes.Count}\n");

            foreach (var factory in _availableBiomes)
            {
                var level = new GameLevel(factory, _player);
                level.PlayLevel();

                if (!_player.IsAlive)
                {
                    Console.WriteLine("\n!!! ИГРА ОКОНЧЕНА !!!");
                    break;
                }

                Console.WriteLine("\n Переход между уровнями \n");
            }

            Console.WriteLine("\n-----------------------");
            Console.WriteLine("   КАМПАНИЯ ЗАВЕРШЕНА");
            Console.WriteLine("-------------------------");
        }
    }
}