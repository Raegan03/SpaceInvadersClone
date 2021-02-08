using JGajewski.Commands;
using JGajewski.Game.States.Gameplay;
using JGajewski.StateMachine;
using Zenject;

namespace JGajewski.Game.States.MainMenu.Commands
{
    public struct StartGameCommand : ICommand
    {
    }

    public class StartGameCommandHandler : CommandHandler<StartGameCommand>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameplayState _gameplayState;
        private readonly MainMenuState _mainMenuState;
        
        [Inject]
        public StartGameCommandHandler(GameStateMachine gameStateMachine, 
            GameplayState gameplayState, MainMenuState mainMenuState)
        {
            _gameStateMachine = gameStateMachine;
            _gameplayState = gameplayState;
            _mainMenuState = mainMenuState;
        }
        
        protected override void ExplicitExecute(StartGameCommand command)
        {
            if(_gameStateMachine.State != _mainMenuState) return;
            _gameStateMachine.ChangeStateAndForget(_gameplayState);
        }
    }
}
