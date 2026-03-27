// Конкретный продукт: враг ледяного биома.
// Реализует интерфейс IEnemy.
// Инкапсулирует поведение и характеристики ледяного противника.

using System;
using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Ice
{
    public class IceEnemy : IEnemy
    {
        public string Name => "Ледяной Голем";
        public int Health { get; set; } = 80;
        public int Damage => 20;

        public void Attack(Player player)
        {
            Console.WriteLine($"  [{Name}] Бьет ледяным кулаком!");
            player.TakeDamage(Damage);
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"  [{Name}] Получил {amount} урона. HP: {Health}");
        }
    }
}