using UnityEngine;

namespace JGajewski.Entities.Projectile.Data
{
    public readonly struct ProjectileSpawnData
    {
        public readonly ProjectileOwnerType ProjectileOwnerType;
        public readonly Vector3 SpawnPosition;
        public readonly Vector3 Direction;
        public readonly float ProjectileSpeed;

        public ProjectileSpawnData(ProjectileOwnerType projectileOwnerType, Vector3 spawnPosition, 
            Vector3 direction, float projectileSpeed)
        {
            ProjectileOwnerType = projectileOwnerType;
            SpawnPosition = spawnPosition;
            Direction = direction;
            ProjectileSpeed = projectileSpeed;
        }
    }
}
