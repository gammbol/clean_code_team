// Интерфейс продукта: оружие.
// Определяет контракт для всех видов оружия в семействах биома.
// Позволяет клиенту использовать оружие без знания конкретной реализации.

using afabric_game.Models;

namespace afabric_game.Interfaces
{
    public interface IWeapon
    {
        string Name { get; }
        int Damage { get; }
        float Durability { get; set; }
        void Use(Player player, IEnemy enemy);
    }
}