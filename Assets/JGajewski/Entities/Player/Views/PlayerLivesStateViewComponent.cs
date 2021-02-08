using Cysharp.Threading.Tasks;
using JGajewski.Entities.Player.Signals;
using JGajewski.StateMachine.Abstracts.Views;
using TMPro;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Player.Views
{
    public class PlayerLivesStateViewComponent : StateView.StateViewComponent
    {
        [SerializeField] private TextMeshProUGUI playerLivesLabel;
        
        private SignalBus _signalBus;
    
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override async UniTask Enter()
        {
            _signalBus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        }

        public override async UniTask Exit()
        {
            _signalBus.Unsubscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        }
        
        private void OnPlayerDamaged(PlayerDamagedSignal s)
        {
            playerLivesLabel.text = $"Player lives: {s.CurrentPlayerLives}";
        }
    }
}
