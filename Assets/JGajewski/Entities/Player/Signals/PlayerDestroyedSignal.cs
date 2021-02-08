using System;
using JGajewski.Entities.Interfaces.Signals;

namespace JGajewski.Entities.Player.Signals
{
    public class PlayerDestroyedSignal : IEntityDestroyedSignal
    {
        public Guid EntityGuid { get; }

        public PlayerDestroyedSignal(Guid entityGuid)
        {
            EntityGuid = entityGuid;
        }
    }
}
