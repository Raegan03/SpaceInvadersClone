using System.Collections.Generic;
using JGajewski.Commands;
using JGajewski.Entities.Projectile.Commands;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Factories;
using JGajewski.Entities.Projectile.Signals;
using JGajewski.Entities.Projectile.Systems;
using JGajewski.Entities.Projectile.Views;
using Zenject;

namespace JGajewski.Entities.Projectile
{
    public class ProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProjectilesSystemsManager>()
                .AsSingle();
            
            Container.BindEntitySystem<ProjectilesSystemsManager, ProjectilesMoveSystem, IReadOnlyList<ProjectileEntity>>();

            Container.BindInterfacesTo<ProjectileEntityPrefabProvider>()
                .AsSingle();

            Container.BindEntityMonoPool<ProjectileView, ProjectileSpawnData, 
                ProjectilesMonoPool, ProjectilesFactory>();

            Container.BindCommandHandler<CheckProjectileTriggerCommand, CheckProjectileTriggerCommandHandler>();
            Container.BindCommandHandler<FireProjectileCommand, FireProjectileCommandHandler>();

            Container.DeclareSignal<ProjectileCreatedSignal>();
            Container.DeclareSignal<ProjectileDestroyedSignal>();
            Container.DeclareSignal<ProjectilesMovedSignal>();
        }
    }
}