using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Projectile.Commands;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Views
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileTrigger : EntityViewComponent
    {
        [SerializeField] private Collider projectileCollider;
        
        protected override void OnPopulated()
        {
            projectileCollider.enabled = true;
        }

        protected override void OnCleared()
        {
            projectileCollider.enabled = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            EntityView.CommandBus.Send(
                new CheckProjectileTriggerCommand(EntityView.EntityGuid, other));
        }
    }
}
