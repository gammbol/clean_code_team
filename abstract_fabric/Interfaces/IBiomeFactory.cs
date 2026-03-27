// Абстрактная фабрика (Abstract Factory).
// Интерфейс для создания семейств связанных объектов: враг, оружие, предмет, карта.
// Гарантирует что созданные объекты принадлежат одному биому и совместимы между собой.

namespace afabric_game.Interfaces
{
    public interface IBiomeFactory
    {
        IEnemy CreateEnemy();
        IWeapon CreateWeapon();
        IEnvironmentItem CreateItem();
        ILevelMap CreateMap();
        string BiomeName { get; }
    }
}