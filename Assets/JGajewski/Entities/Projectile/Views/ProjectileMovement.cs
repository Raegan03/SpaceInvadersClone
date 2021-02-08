using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Projectile.Signals;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Views
{
    public class ProjectileMovement : EntityViewComponent
    {
        [SerializeField] private Transform entityTransform;
        
        protected override void OnPopulated()
        {
            EntityView.SignalBus.Subscribe<ProjectilesMovedSignal>(HandleProjectileMoved);
        }

        protected override void OnCleared()
        {
            EntityView.SignalBus.Unsubscribe<ProjectilesMovedSignal>(HandleProjectileMoved);
        }

        protected virtual void HandleProjectileMoved(ProjectilesMovedSignal s)
        {
            if(!s.ProjectilesPositions.TryGetValue(EntityView.EntityGuid, out var position)) return;
            entityTransform.position = position;
        }
    }
}
