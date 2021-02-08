using System;
using JGajewski.Entities.Interfaces.Signals;

namespace JGajewski.Entities.Enemies.Signals
{
    public class EnemyDestroyedSignal : IEntityDestroyedSignal
    {
        public Guid EntityGuid { get; }

        public EnemyDestroyedSignal(Guid entityGuid)
        {
            EntityGuid = entityGuid;
        }
    }
}
