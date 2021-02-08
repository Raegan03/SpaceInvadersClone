using System;
using JGajewski.Entities.Interfaces;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Data
{
    public class ProjectileEntity : IEntity
    {
        public Guid EntityGuid { get; }
        
        public ProjectileOwnerType ProjectileOwnerType { get; }
        
        public Vector3 ProjectilePosition { get; private set; }
        public Vector3 ProjectileDirection { get; }
        public float ProjectileSpeed { get; }

        public ProjectileEntity(ProjectileSpawnData projectileSpawnData)
        {
            EntityGuid = Guid.NewGuid();

            ProjectileOwnerType = projectileSpawnData.ProjectileOwnerType;
            
            ProjectilePosition = new Vector3(projectileSpawnData.SpawnPosition.x, 0f, projectileSpawnData.SpawnPosition.z);
            ProjectileDirection = projectileSpawnData.Direction;
            
            ProjectileSpeed = projectileSpawnData.ProjectileSpeed;
        }

        public void UpdatePosition(float delta)
        {
            ProjectilePosition += ProjectileDirection * ProjectileSpeed * delta;
        }
    }
}
