using System;
using JGajewski.Entities.Interfaces.Signals;

namespace JGajewski.Entities.Enemies.Signals
{
    public class EnemiesWaveDestroyedSignal : IEntityDestroyedSignal
    {
        public Guid EntityGuid { get; }

        public EnemiesWaveDestroyedSignal(Guid entityGuid)
        {
            EntityGuid = entityGuid;
        }
    }
}
