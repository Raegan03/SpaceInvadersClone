using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Player.Signals;
using UnityEngine;

namespace JGajewski.Entities.Player.Systems
{
    public class PlayerLivesSystem : EntityUpdateSystem<PlayerSystemsManager, PlayerEntity>
    {
        private float _protectionTime;
        
        private readonly PlayerSettings _playerSettings;
        
        public PlayerLivesSystem(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
        }
        
        public override void Start(PlayerSystemsManager manager, PlayerEntity data)
        {
            base.Start(manager, data);
            
            _protectionTime = 0f;
            
            Data.OnPlayerDamaged += HandlePlayerDamaged;
            Data.OnPlayerProtectionChanged += HandlePlayerProtectionChanged;
            
            Manager.SignalBus.Fire(new PlayerDamagedSignal(Data.PlayerLives));
        }

        public override void Stop()
        {
            Data.OnPlayerDamaged -= HandlePlayerDamaged;
            Data.OnPlayerProtectionChanged -= HandlePlayerProtectionChanged;

            base.Stop();
        }

        public override void Update()
        {
            base.Update();
            
            if(!Data.PlayerProtected) return;

            _protectionTime += Time.deltaTime;
            if (_protectionTime >= _playerSettings.PlayerProtectionDuration)
            {
                Data.SetProtection(false);
                _protectionTime = 0f;
            }
        }

        private void HandlePlayerDamaged(int playerLives)
        {
            Manager.SignalBus.Fire(new PlayerDamagedSignal(playerLives));
            if (playerLives == 0)
            {
                Manager.SignalBus.AbstractFire<PlayerDiedSignal>();
                return;
            }
            Data.SetProtection(true);
        }
        
        private void HandlePlayerProtectionChanged(bool playerProtected)
        {
            Manager.SignalBus.Fire(new PlayerProtectionChangedSignal(playerProtected));
        }
    }
}
