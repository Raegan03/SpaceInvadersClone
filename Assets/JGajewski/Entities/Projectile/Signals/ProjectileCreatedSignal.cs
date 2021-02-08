using System;
using JGajewski.Entities.Interfaces.Signals;
using JGajewski.Entities.Projectile.Data;

namespace JGajewski.Entities.Projectile.Signals
{
    public class ProjectileCreatedSignal : IEntityCreatedSignal<ProjectileSpawnData>
    {
        public Guid EntityGuid { get; }
        public ProjectileSpawnData P1 { get; }
        
        public Guid OwnerGuid { get; }

        public ProjectileCreatedSignal(Guid entityGuid, ProjectileSpawnData projectileSpawnData, Guid ownerGuid)
        {
            EntityGuid = entityGuid;
            P1 = projectileSpawnData;

            OwnerGuid = ownerGuid;
        }
    }
}
