// Конкретный продукт: предмет пустынного биома.
// Реализует интерфейс IEnvironmentItem.
// Инкапсулирует логику сбора и применения эффекта ресурса пустынного типа.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Desert
{
    public class DesertItem : IEnvironmentItem
    {
        public string Name => "Фляга с Водой";
        public string ResourceType => "Consumable";
        public int Value => 30;

        public void Collect(Player player)
        {
            player.Inventory.Add(Name);
            player.Heal(Value);
            Console.WriteLine($"  [Собрано] {Name} восстановил {Value} HP");
        }
    }
}