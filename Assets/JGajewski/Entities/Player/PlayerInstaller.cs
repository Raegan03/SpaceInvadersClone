using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Player.Factories;
using JGajewski.Entities.Player.Signals;
using JGajewski.Entities.Player.Systems;
using JGajewski.Entities.Player.Views;
using Zenject;

namespace JGajewski.Entities.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerSystemsManager>()
                .AsSingle();

            Container.BindEntitySystem<PlayerSystemsManager, PlayerMoveSystem, PlayerEntity>();
            Container.BindEntitySystem<PlayerSystemsManager, PlayerShootingSystem, PlayerEntity>();
            Container.BindEntitySystem<PlayerSystemsManager, PlayerLivesSystem, PlayerEntity>();

            Container.BindInterfacesTo<PlayerPrefabProvider>()
                .AsSingle();

            Container.BindEntityFactory<PlayerView, PlayerFactory, PlayerEntityFactory>();

            Container.DeclareSignal<PlayerCreatedSignal>();
            Container.DeclareSignal<PlayerDestroyedSignal>();
            Container.DeclareSignal<PlayerMovedSignal>();
            Container.DeclareSignal<PlayerShootSignal>();

            Container.DeclareSignal<PlayerDamagedSignal>();
            Container.DeclareSignal<PlayerProtectionChangedSignal>();
            
            Container.DeclareSignalWithInterfaces<PlayerDiedSignal>()
                .OptionalSubscriber();
        }
    }
}