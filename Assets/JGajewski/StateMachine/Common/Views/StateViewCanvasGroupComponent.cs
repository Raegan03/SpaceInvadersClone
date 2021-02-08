using Cysharp.Threading.Tasks;
using JGajewski.Common;
using JGajewski.Game.Data;
using JGajewski.StateMachine.Abstracts.Views;
using UnityEngine;
using Zenject;

namespace JGajewski.StateMachine.Common.Views
{
    public class StateViewCanvasGroupComponent : StateView.StateViewComponent
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private GameSettings _gameSettings;
        
        [Inject]
        private void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        public override async UniTask Enter()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            await canvasGroup.FadeGroup(0f, 1f, _gameSettings.TransitionFadesDuration);
            
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public override async UniTask Exit()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            
            await canvasGroup.FadeGroup(1f, 0f, _gameSettings.TransitionFadesDuration);
        }
    }
}
