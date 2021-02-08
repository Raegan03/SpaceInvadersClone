using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Factories;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Entities.Enemies.Systems;
using JGajewski.Entities.Enemies.Views;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Enemies
{
    public class EnemiesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemiesSystemsManager>()
                .AsSingle();
            
            Container.BindEntitySystem<EnemiesSystemsManager, EnemiesWaveEntitiesSystem, EnemiesWaveEntity>();
            Container.BindEntitySystem<EnemiesSystemsManager, EnemiesWaveMaintainerSystem, EnemiesWaveEntity>();
            Container.BindEntitySystem<EnemiesSystemsManager, EnemiesWaveMoveSystem, EnemiesWaveEntity>();
            Container.BindEntitySystem<EnemiesSystemsManager, EnemiesShootingSystem, EnemiesWaveEntity>();
            
            Container.BindInterfacesTo<EnemiesPrefabsLoader>()
                .AsSingle();
            
            Container.BindEntityMonoPool<EnemyView, Vector3, EnemyType, EnemiesMonoPool, EnemiesEntityFactory>();
            Container.BindEntityFactory<EnemiesWaveView, EnemiesWavePlaceholderFactory, EnemiesWaveFactory>();
            
            Container.DeclareSignal<EnemiesWaveCreatedSignal>();
            Container.DeclareSignal<EnemiesWaveDestroyedSignal>();
            Container.DeclareSignal<EnemiesWaveMovedSignal>();
            
            Container.DeclareSignalWithInterfaces<EnemiesWaveReachedPlayerSignal>()
                .OptionalSubscriber();
            
            Container.DeclareSignal<EnemyCreatedSignal>();
            Container.DeclareSignal<EnemyDestroyedSignal>();
            Container.DeclareSignal<EnemyShootSignal>();
        }
    }
}
