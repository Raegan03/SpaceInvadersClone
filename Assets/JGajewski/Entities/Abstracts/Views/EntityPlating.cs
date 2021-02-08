using JGajewski.Entities.Commands;
using UnityEngine;

namespace JGajewski.Entities.Abstracts.Views
{
    [RequireComponent(typeof(Collider))]
    public abstract class EntityPlating : EntityViewComponent
    {
        protected abstract EntityType EntityType { get; }
        
        protected override void OnPopulated()
        {
        }

        protected override void OnCleared()
        {
        } 
        
        public virtual void PlatingShoot()
        {
            EntityView.CommandBus.Send(new DamageEntityCommand(
                EntityView.EntityGuid, EntityType));
        }
    }
}
