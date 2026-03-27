// Конкретный продукт: карта лесного биома.
// Реализует интерфейс ILevelMap.
// Определяет особенности ландшафта и экологического воздействия леса.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Forest
{
    public class ForestMap : ILevelMap
    {
        public string TerrainType => "Густой Лес";
        public float DifficultyMultiplier => 1.0f;

        public void Render()
        {
            Console.WriteLine("  [Карта] Отрисовка: Деревья, Трава, Река");
        }

        public float CalculateEnvironmentalDamage(Player player)
        {
            return 0;
        }
    }
}