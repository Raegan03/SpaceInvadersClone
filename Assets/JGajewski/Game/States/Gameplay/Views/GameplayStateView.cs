using JGajewski.Game.States.Gameplay.Interfaces;
using JGajewski.Game.States.Gameplay.Signals;
using JGajewski.StateMachine.Abstracts.Views;
using JGajewski.StateMachine.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.Gameplay.Views
{
    public class GameplayStateView : StateView, IGameplayStateAction
    {
        public override int Priority { get; } = -1;
        
        [SerializeField] private TextMeshProUGUI gameplayScoreLabel;
        [SerializeField] private TextMeshProUGUI gameplayWavesLabel;
        
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void PreEnterAction(IState currentState)
        {
            _signalBus.Subscribe<GameplayScoreChangedSignal>(HandleGameplayScoreChanged);
            _signalBus.Subscribe<GameplayCurrentWaveChangedSignal>(HandleGameplayWavesChanged);
            
            base.PreEnterAction(currentState);
        }

        public override void PreExitAction(IState currentState)
        {
            _signalBus.Unsubscribe<GameplayScoreChangedSignal>(HandleGameplayScoreChanged);
            _signalBus.Unsubscribe<GameplayCurrentWaveChangedSignal>(HandleGameplayWavesChanged);
            
            base.PreExitAction(currentState);
        }

        private void HandleGameplayScoreChanged(GameplayScoreChangedSignal s)
        {
            gameplayScoreLabel.text = $"Score: {s.Score}";
        }
        
        private void HandleGameplayWavesChanged(GameplayCurrentWaveChangedSignal s)
        {
            gameplayWavesLabel.text = $"Current wave: {s.Waves}";
        }
    }
}
