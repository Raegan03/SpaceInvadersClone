using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Interfaces.Factories;
using JGajewski.Entities.Projectile.Views;
using Zenject;

namespace JGajewski.Entities.Projectile.Factories
{
    public class ProjectilesFactory : EntityFactory<ProjectileView>
    {
        [Inject]
        public ProjectilesFactory(DiContainer container, IEntityPrefabProvider<ProjectileView> entityPrefabProvider) 
            : base(container, entityPrefabProvider)
        {
        }
    }
}
