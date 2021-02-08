using Cysharp.Threading.Tasks;
using JGajewski.Commands;
using JGajewski.Game.States.MainMenu.Commands;
using JGajewski.StateMachine.Abstracts.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGajewski.Game.States.MainMenu.Views
{
    public class MainMenuStateViewInputComponent : StateView.StateViewComponent
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button highScoreButton;
        
        private CommandBus _commandBus;
        
        private readonly StartGameCommand _startGameCommand = new StartGameCommand();
        private readonly OpenHighScoresCommand _openHighScoresCommand = new OpenHighScoresCommand();

        [Inject]
        private void Construct(CommandBus commandBus)
        {
            _commandBus = commandBus;
        }
        
        public override async UniTask Enter()
        {
            startGameButton.onClick.AddListener(HandleStartGameClicked);
            highScoreButton.onClick.AddListener(HandleHighScoreClicked);
        }

        public override async UniTask Exit()
        {
            startGameButton.onClick.RemoveListener(HandleStartGameClicked);
            highScoreButton.onClick.RemoveListener(HandleHighScoreClicked);
        }
        
        private void HandleStartGameClicked()
        {
            _commandBus.Send(_startGameCommand);
        }
        
        private void HandleHighScoreClicked()
        {
            _commandBus.Send(_openHighScoresCommand);
        }
    }
}
