using JGajewski.Commands;
using JGajewski.Game.States.MainMenu;
using JGajewski.StateMachine;
using Zenject;

namespace JGajewski.Game.States.Loading.Commands
{
    public struct LoadingProceedCommand : ICommand
    {
    }

    public class LoadingProceedCommandHandler : CommandHandler<LoadingProceedCommand>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly MainMenuState _mainMenuState;
        private readonly LoadingState _loadingState;
        
        [Inject]
        public LoadingProceedCommandHandler(GameStateMachine gameStateMachine, 
            LoadingState loadingState, MainMenuState mainMenuState)
        {
            _gameStateMachine = gameStateMachine;
            _mainMenuState = mainMenuState;
            _loadingState = loadingState;
        }
        
        protected override void ExplicitExecute(LoadingProceedCommand command)
        {
            if(_gameStateMachine.State != _loadingState) return;
            _gameStateMachine.ChangeStateAndForget(_mainMenuState);
        }
    }
}
