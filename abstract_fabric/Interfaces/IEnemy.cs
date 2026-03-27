// Интерфейс продукта: враг.
// Определяет контракт для всех конкретных врагов в семействах биома.
// Используется клиентским кодом через абстракцию.

using afabric_game.Models;

namespace afabric_game.Interfaces
{
    public interface IEnemy
    {
        string Name { get; }
        int Health { get; set; }
        int Damage { get; }
        void Attack(Player player);
        void TakeDamage(int amount);
    }
}