using JGajewski.Commands;
using JGajewski.Game.States;
using JGajewski.Game.States.MainMenu;
using JGajewski.StateMachine;
using Zenject;

namespace JGajewski.Game.Commands
{
    public struct BackToMainMenuCommand : ICommand
    {
    }

    public class BackToMainMenuCommandHandler : CommandHandler<BackToMainMenuCommand>
    {
        private GameStateMachine _gameStateMachine;
        private MainMenuState _mainMenuState;

        [Inject]
        public BackToMainMenuCommandHandler(GameStateMachine gameStateMachine, MainMenuState mainMenuState)
        {
            _gameStateMachine = gameStateMachine;
            _mainMenuState = mainMenuState;
        }
        
        protected override void ExplicitExecute(BackToMainMenuCommand command)
        {
            if(_gameStateMachine.State == _mainMenuState) return;
            _gameStateMachine.ChangeStateAndForget(_mainMenuState);
        }
    }
}
