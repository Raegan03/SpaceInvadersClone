using System;
using JGajewski.Commands;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Systems;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Projectile.Commands
{
    public readonly struct FireProjectileCommand : ICommand
    {
        public readonly Guid OwnerGuid;
        public readonly ProjectileOwnerType ProjectileOwnerType;
        
        public readonly Vector3 ProjectilePosition;
        public readonly Vector3 ProjectileDirection;
        public readonly float ProjectilePower;

        public FireProjectileCommand(Guid ownerGuid, ProjectileOwnerType projectileOwnerType, 
            Vector3 projectilePosition, Vector3 projectileDirection, float projectilePower)
        {
            OwnerGuid = ownerGuid;
            ProjectileOwnerType = projectileOwnerType;
            
            ProjectilePosition = projectilePosition;
            ProjectileDirection = projectileDirection;
            ProjectilePower = projectilePower;
        }
    }

    public class FireProjectileCommandHandler : CommandHandler<FireProjectileCommand>
    {
        private readonly ProjectilesSystemsManager _projectilesSystemsManager;
        
        [Inject]
        public FireProjectileCommandHandler(ProjectilesSystemsManager projectilesSystemsManager)
        {
            _projectilesSystemsManager = projectilesSystemsManager;
        }
        
        protected override void ExplicitExecute(FireProjectileCommand command)
        {
            var projectileSpawnData = new ProjectileSpawnData(command.ProjectileOwnerType, command.ProjectilePosition,
                command.ProjectileDirection, command.ProjectilePower);
            
            _projectilesSystemsManager.CreateProjectile(command.OwnerGuid, projectileSpawnData);
        }
    }
}
