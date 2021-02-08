using JGajewski.Commands;
using JGajewski.Game.Commands;
using JGajewski.Game.Database;
using JGajewski.Game.States;
using Zenject;

namespace JGajewski.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindCommandBus();

            Container.Bind<GameStateMachine>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<HighScoresDatabase>().AsSingle();
            
            Container.BindCommandHandler<LoadGameCommand, LoadGameCommandHandler>();
            Container.BindCommandHandler<BackToMainMenuCommand, BackToMainMenuCommandHandler>();
            Container.BindCommandHandler<SubmitGameplayResultCommand, SubmitGameplayResultCommandHandler>();
            Container.BindCommandHandler<SaveHighScoreCommand, SaveHighScoreCommandHandler>();
        }
    }
}