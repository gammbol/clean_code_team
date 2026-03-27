// Конкретный продукт: враг пустынного биома.
// Реализует интерфейс IEnemy.
// Инкапсулирует поведение и характеристики пустынного противника.

using System;
using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Desert
{
    public class DesertEnemy : IEnemy
    {
        public string Name => "Пустынный Скорпион";
        public int Health { get; set; } = 40;
        public int Damage => 15;

        public void Attack(Player player)
        {
            Console.WriteLine($"  [{Name}] Жалит игрока ядом!");
            player.TakeDamage(Damage);
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            Console.WriteLine($"  [{Name}] Получил {amount} урона. HP: {Health}");
        }
    }
}