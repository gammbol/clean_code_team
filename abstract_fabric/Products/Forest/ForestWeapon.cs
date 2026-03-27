// Конкретный продукт: оружие лесного биома.
// Реализует интерфейс IWeapon.
// Инкапсулирует логику атаки и износа для оружия лесного стиля.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Forest
{
    public class ForestWeapon : IWeapon
    {
        public string Name => "Деревянный Лук";
        public int Damage => 15;
        public float Durability { get; set; } = 100f;

        public void Use(Player player, IEnemy enemy)
        {
            if (Durability <= 0)
            {
                Console.WriteLine($"  [{Name}] Сломан! Нельзя атаковать.");
                return;
            }
            Console.WriteLine($"  [{Name}] Выстрел в {enemy.Name}!");
            enemy.TakeDamage(Damage);
            Durability -= 5f;
        }
    }
}