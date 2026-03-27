// Конкретный продукт: карта пустынного биома.
// Реализует интерфейс ILevelMap.
// Определяет особенности ландшафта и экологического воздействия пустыни.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Desert
{
    public class DesertMap : ILevelMap
    {
        public string TerrainType => "Жаркая Пустыня";
        public float DifficultyMultiplier => 1.5f;

        public void Render()
        {
            Console.WriteLine("  [Карта] Отрисовка: Песок, Кактусы, Солнце");
        }

        public float CalculateEnvironmentalDamage(Player player)
        {
            Console.WriteLine("  [Среда] Солнечный удар! Игрок теряет HP");
            return 5f;
        }
    }
}