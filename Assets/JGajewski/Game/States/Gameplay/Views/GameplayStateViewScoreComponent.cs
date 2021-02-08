using Cysharp.Threading.Tasks;
using JGajewski.Game.States.Gameplay.Signals;
using JGajewski.StateMachine.Abstracts.Views;
using TMPro;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.Gameplay.Views
{
    public class GameplayStateViewScoreComponent : StateView.StateViewComponent
    {
        [SerializeField] private TextMeshProUGUI gameplayScoreLabel;
        
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override async UniTask Enter()
        {
            _signalBus.Subscribe<GameplayScoreChangedSignal>(HandleGameplayScoreChanged);
        }

        public override async UniTask Exit()
        {
            _signalBus.Unsubscribe<GameplayScoreChangedSignal>(HandleGameplayScoreChanged);
        }
        
        private void HandleGameplayScoreChanged(GameplayScoreChangedSignal s)
        {
            gameplayScoreLabel.text = $"Score: {s.Score}";
        }
    }
}
