using JGajewski.Entities.Abstracts.Spawners;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Signals;
using JGajewski.Entities.Projectile.Views;

namespace JGajewski.Entities.Projectile.Factories
{
    public class ProjectileMonoPoolSpawner : EntityMonoPoolSpawner<ProjectileSpawnData, ProjectilesMonoPool, 
        ProjectileView, ProjectileCreatedSignal, ProjectileDestroyedSignal>
    {
    }
}
