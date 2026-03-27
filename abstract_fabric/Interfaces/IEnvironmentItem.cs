// Интерфейс продукта: предмет окружения.
// Определяет контракт для собираемых ресурсов в семействах биома.
// Инкапсулирует логику сбора и применения эффекта к игроку.

using afabric_game.Models;

namespace afabric_game.Interfaces
{
    public interface IEnvironmentItem
    {
        string Name { get; }
        string ResourceType { get; }
        int Value { get; }
        void Collect(Player player);
    }
}