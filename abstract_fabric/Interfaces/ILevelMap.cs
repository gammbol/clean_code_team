// Интерфейс продукта: карта уровня.
// Определяет контракт для ландшафта в семействах биома.
// Отвечает за отрисовку и расчет экологического урона.

using afabric_game.Models;

namespace afabric_game.Interfaces
{
    public interface ILevelMap
    {
        string TerrainType { get; }
        float DifficultyMultiplier { get; }
        void Render();
        float CalculateEnvironmentalDamage(Player player);
    }
}