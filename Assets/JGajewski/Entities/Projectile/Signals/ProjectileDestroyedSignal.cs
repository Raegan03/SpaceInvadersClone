using System;
using JGajewski.Entities.Interfaces.Signals;

namespace JGajewski.Entities.Projectile.Signals
{
    public class ProjectileDestroyedSignal : IEntityDestroyedSignal
    {
        public Guid EntityGuid { get; }

        public ProjectileDestroyedSignal(Guid entityGuid)
        {
            EntityGuid = entityGuid;
        }
    }
}
