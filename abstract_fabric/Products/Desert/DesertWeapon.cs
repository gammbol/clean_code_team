// Конкретный продукт: оружие пустынного биома.
// Реализует интерфейс IWeapon.
// Инкапсулирует логику атаки и износа для оружия пустынного стиля.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Desert
{
    public class DesertWeapon : IWeapon
    {
        public string Name => "Песчаный Кинжал";
        public int Damage => 20;
        public float Durability { get; set; } = 80f;

        public void Use(Player player, IEnemy enemy)
        {
            if (Durability <= 0)
            {
                Console.WriteLine($"  [{Name}] Затупился! Нельзя атаковать.");
                return;
            }
            Console.WriteLine($"  [{Name}] Удар по {enemy.Name}!");
            enemy.TakeDamage(Damage);
            Durability -= 8f;
        }
    }
}