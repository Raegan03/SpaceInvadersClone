using JGajewski.Entities.Interfaces.Signals;
using JGajewski.Entities.Projectile.Commands;
using JGajewski.Entities.Projectile.Data;
using UnityEngine;

namespace JGajewski.Entities.Abstracts.Views
{
    public abstract class EntityWeapon<TEntityShootSignal> : EntityViewComponent
        where TEntityShootSignal : IEntityShootSignal
    {
        public abstract ProjectileOwnerType ProjectileOwnerType { get; }
        
        [SerializeField] private Transform shootTransform;
        
        protected override void OnPopulated()
        {
            EntityView.SignalBus.Subscribe<TEntityShootSignal>(HandleEntityShoot);
        }

        protected override void OnCleared()
        {
            EntityView.SignalBus.Unsubscribe<TEntityShootSignal>(HandleEntityShoot);
        }

        protected virtual void HandleEntityShoot(TEntityShootSignal s)
        {
            if(EntityView.EntityGuid != s.EntityGuid) return;
            EntityView.CommandBus.Send(new FireProjectileCommand(EntityView.EntityGuid, ProjectileOwnerType, 
                shootTransform.position, shootTransform.forward, s.ShootSpeed));
        }
    }
}