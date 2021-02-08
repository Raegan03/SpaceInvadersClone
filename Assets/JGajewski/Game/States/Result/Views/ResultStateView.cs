using JGajewski.Game.States.Result.Interfaces;
using JGajewski.Game.States.Result.Signals;
using JGajewski.StateMachine.Abstracts.Views;
using JGajewski.StateMachine.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.Result.Views
{
    public class ResultStateView : StateView, IResultStateAction
    {
        public override int Priority => -1;

        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI wavesCleared;

        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void PreEnterAction(IState currentState)
        {
            base.PreEnterAction(currentState);
            
            _signalBus.Subscribe<ResultDataSubmittedSignal>(HandleResultDataSubmitted);
        }

        public override void PreExitAction(IState currentState)
        {
            _signalBus.Unsubscribe<ResultDataSubmittedSignal>(HandleResultDataSubmitted);
        }

        private void HandleResultDataSubmitted(ResultDataSubmittedSignal s)
        {
            score.text = $"Score: {s.Score}";
            wavesCleared.text = $"Waves Cleared: {s.WavesCleared}";
        }
    }
}
