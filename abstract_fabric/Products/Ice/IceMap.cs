// Конкретный продукт: карта ледяного биома.
// Реализует интерфейс ILevelMap.
// Определяет особенности ландшафта и экологического воздействия ледяного мира.

using afabric_game.Interfaces;
using afabric_game.Models;

namespace afabric_game.Products.Ice
{
    public class IceMap : ILevelMap
    {
        public string TerrainType => "Вечная Мерзлота";
        public float DifficultyMultiplier => 2.0f;

        public void Render()
        {
            Console.WriteLine("  [Карта] Отрисовка: Лед, Снег, Айсберги");
        }

        public float CalculateEnvironmentalDamage(Player player)
        {
            Console.WriteLine("  [Среда] Обморожение! Игрок теряет HP");
            return 8f;
        }
    }
}