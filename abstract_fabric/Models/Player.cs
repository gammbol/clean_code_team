// Модель данных - игрок.
// Содержит состояние персонажа (здоровье, инвентарь, оружие) и базовые действия.
// Не зависит от конкретных классов биома, работает только с интерфейсами.

using System;
using System.Collections.Generic;
using afabric_game.Interfaces;

namespace afabric_game.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public IWeapon? CurrentWeapon { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
        public bool IsAlive => Health > 0;

        public void TakeDamage(int amount)
        {
            Health = Math.Max(0, Health - amount);
            Console.WriteLine($"  [Игрок] Получил {amount} урона. Осталось HP: {Health}");
        }

        public void Heal(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
            Console.WriteLine($"  [Игрок] Восстановил {amount} HP. Текущее HP: {Health}");
        }
    }
}