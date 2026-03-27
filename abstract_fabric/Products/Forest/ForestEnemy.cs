// Конкретный продукт: враг лесного биома.
// Реализует интерфейс IEnemy.
// Инкапсулирует поведение и характеристики лесного противника.

using System;
using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Forest
{
    public class ForestEnemy : IEnemy
    {
        public string Name => "Лесной Волк";
        public int Health { get; set; } = 50;
        public int Damage => 10;

        public void Attack(Player player)
        {
            Console.WriteLine($"  [{Name}] Нападает и кусает игрока!");
            player.TakeDamage(Damage);
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"  [{Name}] Получил {amount} урона. HP: {Health}");
        }
    }
}