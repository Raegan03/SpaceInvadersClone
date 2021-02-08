using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Enemies.Views;
using JGajewski.Entities.Interfaces.Factories;
using Zenject;

namespace JGajewski.Entities.Enemies.Factories
{
    public class EnemiesEntityFactory : EntityFactory<EnemyView>
    {
        [Inject]
        public EnemiesEntityFactory(DiContainer container, IEntityPrefabProvider<EnemyView> entityPrefabProvider) 
            : base(container, entityPrefabProvider)
        {
        }
    }
}
