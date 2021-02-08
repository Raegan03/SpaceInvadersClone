using System;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;

namespace JGajewski.Entities.Player.Signals
{
    public class PlayerMovedSignal : IEntityMovedSignal
    {
        public Guid EntityGuid { get; }
        public Vector3 EntityPosition { get; }

        public PlayerMovedSignal(Guid entityGuid, Vector3 entityPosition)
        {
            EntityGuid = entityGuid;
            EntityPosition = entityPosition;
        }
    }
}
