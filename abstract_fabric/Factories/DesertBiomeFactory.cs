// Конкретная фабрика (Concrete Factory): создание семейства объектов пустынного биома.
// Реализует интерфейс IBiomeFactory.
// Гарантирует, что все созданные объекты совместимы и принадлежат пустынному стилю.

using afabric_game.Interfaces;
using afabric_game.Products.Desert;

namespace afabric_game.Factories
{
    public class DesertBiomeFactory : IBiomeFactory
    {
        public string BiomeName => "Пустынный Биом";
        public IEnemy CreateEnemy() => new DesertEnemy();
        public IWeapon CreateWeapon() => new DesertWeapon();
        public IEnvironmentItem CreateItem() => new DesertItem();
        public ILevelMap CreateMap() => new DesertMap();
    }
}