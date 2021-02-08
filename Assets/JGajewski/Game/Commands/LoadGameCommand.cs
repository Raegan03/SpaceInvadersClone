using JGajewski.Commands;
using JGajewski.Game.States;
using JGajewski.Game.States.Loading;
using JGajewski.StateMachine;
using Zenject;

namespace JGajewski.Game.Commands
{
    public struct LoadGameCommand : ICommand
    {
    }

    public class LoadGameCommandHandler : CommandHandler<LoadGameCommand>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingState _loadingState;
        
        [Inject]
        public LoadGameCommandHandler(GameStateMachine gameStateMachine, LoadingState loadingState)
        {
            _gameStateMachine = gameStateMachine;
            _loadingState = loadingState;
        }
        
        protected override void ExplicitExecute(LoadGameCommand command)
        {
            _gameStateMachine.ChangeStateAndForget(_loadingState);
        }
    }
}
