using System;
using JGajewski.Commands;
using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Projectile.Systems;
using JGajewski.Entities.Projectile.Views;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Projectile.Commands
{
    public struct CheckProjectileTriggerCommand : ICommand
    {
        public readonly Guid ProjectileGuid;
        public readonly Collider HitCollider;

        public CheckProjectileTriggerCommand(Guid projectileGuid, Collider hitCollider)
        {
            ProjectileGuid = projectileGuid;
            HitCollider = hitCollider;
        }
    }

    public class CheckProjectileTriggerCommandHandler : CommandHandler<CheckProjectileTriggerCommand>
    {
        private readonly ProjectilesSystemsManager _projectilesSystemsManager;
        
        [Inject]
        public CheckProjectileTriggerCommandHandler(ProjectilesSystemsManager projectilesSystemsManager)
        {
            _projectilesSystemsManager = projectilesSystemsManager;
        }
        
        protected override void ExplicitExecute(CheckProjectileTriggerCommand command)
        {
            if (command.HitCollider.TryGetComponent<EntityPlating>(out var entityPlating))
            {
                entityPlating.PlatingShoot();
                _projectilesSystemsManager.DestroyProjectile(command.ProjectileGuid);
            }
            else if (command.HitCollider.TryGetComponent<ProjectileDestroyer>(out _))
            {
                _projectilesSystemsManager.DestroyProjectile(command.ProjectileGuid);
            }
        }
    }
}
