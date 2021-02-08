using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;

namespace JGajewski.Entities.Abstracts.Views
{
    public abstract class EntityMovement<TEntityMovedSignal> : EntityViewComponent
        where TEntityMovedSignal : IEntityMovedSignal
    {
        [SerializeField] private Transform entityTransform;
        
        protected override void OnPopulated()
        {
            EntityView.SignalBus.Subscribe<TEntityMovedSignal>(HandleEntityMoved);
        }

        protected override void OnCleared()
        {
            EntityView.SignalBus.Unsubscribe<TEntityMovedSignal>(HandleEntityMoved);
        }

        protected virtual void HandleEntityMoved(TEntityMovedSignal s)
        {
            if(EntityView.EntityGuid != s.EntityGuid) return;
            entityTransform.position = s.EntityPosition;
        }
    }
}
