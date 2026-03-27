// Конкретная фабрика (Concrete Factory): создание семейства объектов лесного биома.
// Реализует интерфейс IBiomeFactory.
// Гарантирует, что все созданные объекты совместимы и принадлежат лесному стилю.

using afabric_game.Interfaces;
using afabric_game.Products.Forest;

namespace afabric_game.Factories
{
    public class ForestBiomeFactory : IBiomeFactory
    {
        public string BiomeName => "Лесной Биом";
        public IEnemy CreateEnemy() => new ForestEnemy();
        public IWeapon CreateWeapon() => new ForestWeapon();
        public IEnvironmentItem CreateItem() => new ForestItem();
        public ILevelMap CreateMap() => new ForestMap();
    }
}