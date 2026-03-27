// Конкретный продукт: оружие ледяного биома.
// Реализует интерфейс IWeapon.
// Инкапсулирует логику атаки и износа для оружия ледяного стиля.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Ice
{
    public class IceWeapon : IWeapon
    {
        public string Name => "Кристаллический Меч";
        public int Damage => 25;
        public float Durability { get; set; } = 120f;

        public void Use(Player player, IEnemy enemy)
        {
            if (Durability <= 0)
            {
                Console.WriteLine($"  [{Name}] Раскололся! Нельзя атаковать.");
                return;
            }
            Console.WriteLine($"  [{Name}] Ледяной удар по {enemy.Name}!");
            enemy.TakeDamage(Damage);
            Durability -= 10f;
        }
    }
}