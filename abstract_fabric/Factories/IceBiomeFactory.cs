// Конкретная фабрика (Concrete Factory): создание семейства объектов ледяного биома.
// Реализует интерфейс IBiomeFactory.
// Гарантирует, что все созданные объекты совместимы и принадлежат ледяному стилю.

using afabric_game.Interfaces;
using afabric_game.Products.Ice;

namespace afabric_game.Factories
{
    public class IceBiomeFactory : IBiomeFactory
    {
        public string BiomeName => "Ледяной Биом";
        public IEnemy CreateEnemy() => new IceEnemy();
        public IWeapon CreateWeapon() => new IceWeapon();
        public IEnvironmentItem CreateItem() => new IceItem();
        public ILevelMap CreateMap() => new IceMap();
    }
}