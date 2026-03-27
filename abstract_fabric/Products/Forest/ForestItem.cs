// Конкретный продукт: предмет лесного биома.
// Реализует интерфейс IEnvironmentItem.
// Инкапсулирует логику сбора и применения эффекта ресурса лесного типа.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Forest
{
    public class ForestItem : IEnvironmentItem
    {
        public string Name => "Ягоды";
        public string ResourceType => "Consumable";
        public int Value => 20;

        public void Collect(Player player)
        {
            player.Inventory.Add(Name);
            player.Heal(Value);
            Console.WriteLine($"  [Собрано] {Name} восстановил {Value} HP");
        }
    }
}