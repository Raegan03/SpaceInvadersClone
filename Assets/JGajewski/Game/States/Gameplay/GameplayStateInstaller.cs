using JGajewski.Commands;
using JGajewski.Game.States.Gameplay.Commands;
using JGajewski.Game.States.Gameplay.Interfaces;
using JGajewski.Game.States.Gameplay.Signals;
using JGajewski.Game.States.Gameplay.Views;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.Gameplay
{
    public class GameplayStateInstaller : ActionStateInstaller<GameplayState, 
        IGameplayStateAction, GameplayStateView>
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindCommandHandler<ChangeGameScoreCommand, AddGameScoreCommandHandler>();
            Container.BindCommandHandler<SubmitWaveClearedCommand, SubmitWaveClearedCommandHandler>();
            
            Container.DeclareSignal<GameplayScoreChangedSignal>();
            Container.DeclareSignal<GameplayCurrentWaveChangedSignal>();
        }
    }
}
