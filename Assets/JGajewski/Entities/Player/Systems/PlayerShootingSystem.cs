using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Player.Signals;
using JGajewski.Entities.Projectile.Signals;
using UnityEngine;

namespace JGajewski.Entities.Player.Systems
{
    public class PlayerShootingSystem : EntityUpdateSystem<PlayerSystemsManager, PlayerEntity>
    {
        public override void Start(PlayerSystemsManager manager, PlayerEntity data)
        {
            base.Start(manager, data);
            
            Manager.SignalBus.Subscribe<ProjectileCreatedSignal>(OnProjectileCreated);
            Manager.SignalBus.Subscribe<ProjectileDestroyedSignal>(OnProjectileDestroyed);
        }

        public override void Stop()
        {
            Manager.SignalBus.Unsubscribe<ProjectileCreatedSignal>(OnProjectileCreated);
            Manager.SignalBus.Unsubscribe<ProjectileDestroyedSignal>(OnProjectileDestroyed);
            
            base.Stop();
        }

        public override void Update()
        {
            base.Update();
            
#if !UNITY_ANDROID || UNITY_EDITOR
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                if(Data.HaveProjectile) return;
                Manager.SignalBus.Fire(new PlayerShootSignal(Data.EntityGuid, Data.PlayerProjectileSpeed));
            }
            
#else

            if (Input.touchCount <= 0) return;

            var touch = Input.GetTouch(0);
            if (touch.position.y >= Screen.height / 2f)
            {
                if(Data.HaveProjectile) return;
                Manager.SignalBus.Fire(new PlayerShootSignal(Data.EntityGuid, Data.PlayerProjectileSpeed));
            }
#endif
        }

        private void OnProjectileCreated(ProjectileCreatedSignal s)
        {
            if(s.OwnerGuid != Data.EntityGuid) return;
            Data.SetPlayerProjectile(s.EntityGuid);
        }
        
        private void OnProjectileDestroyed(ProjectileDestroyedSignal s)
        {
            if(s.EntityGuid != Data.PlayerProjectile) return;
            Data.ClearPlayerProjectile();
        }
    }
}
