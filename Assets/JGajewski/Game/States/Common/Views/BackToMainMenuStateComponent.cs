using Cysharp.Threading.Tasks;
using JGajewski.Commands;
using JGajewski.Game.Commands;
using JGajewski.StateMachine.Abstracts.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JGajewski.Game.States.Common.Views
{
    public class BackToMainMenuStateComponent : StateView.StateViewComponent
    {
        [SerializeField] private Button backToMainMenuButton;
        
        private CommandBus _commandBus;
        private readonly BackToMainMenuCommand _backToMainMenuCommand = 
            new BackToMainMenuCommand();

        [Inject]
        private void Construct(CommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public override async UniTask Enter()
        {
            backToMainMenuButton.onClick.AddListener(HandleBackToMainMenuButtonClicked);
        }

        public override async UniTask Exit()
        {
            backToMainMenuButton.onClick.RemoveListener(HandleBackToMainMenuButtonClicked);
        }

        private void HandleBackToMainMenuButtonClicked()
        {
            _commandBus.Send(_backToMainMenuCommand);
        }
    }
}
