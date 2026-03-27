// Конкретный продукт: предмет ледяного биома.
// Реализует интерфейс IEnvironmentItem.
// Инкапсулирует логику сбора и применения эффекта ресурса ледяного типа.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Ice
{
    public class IceItem : IEnvironmentItem
    {
        public string Name => "Магический Кристалл";
        public string ResourceType => "Artifact";
        public int Value => 50;

        public void Collect(Player player)
        {
            player.Inventory.Add(Name);
            player.Heal(Value);
            Console.WriteLine($"  [Собрано] {Name} восстановил {Value} HP");
        }
    }
}