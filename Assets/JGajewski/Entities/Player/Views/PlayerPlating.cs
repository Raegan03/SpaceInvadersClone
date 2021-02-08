using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Player.Signals;
using UnityEngine;

namespace JGajewski.Entities.Player.Views
{
    public class PlayerPlating : EntityPlating
    {
        [SerializeField] private Animator playerAnimator;
        
        protected override EntityType EntityType => EntityType.Player;

        private readonly int _protectedHash = Animator.StringToHash("Protected");

        protected override void OnPopulated()
        {
            EntityView.SignalBus.Subscribe<PlayerProtectionChangedSignal>(HandlePlayerProtectionChanged);
        }

        protected override void OnCleared()
        {
            EntityView.SignalBus.Unsubscribe<PlayerProtectionChangedSignal>(HandlePlayerProtectionChanged);
        }

        private void HandlePlayerProtectionChanged(PlayerProtectionChangedSignal s)
        {
            playerAnimator.SetBool(_protectedHash, s.ProtectionStatus);
        }
    }
}
