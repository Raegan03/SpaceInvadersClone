using System;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;

namespace JGajewski.Entities.Player.Signals
{
    public class PlayerCreatedSignal : IEntityCreatedSignal<Vector3>
    {
        public Guid EntityGuid { get; }
        
        public Vector3 P1 { get; }

        public PlayerCreatedSignal(Guid entityGuid, Vector3 playerSpawnPosition)
        {
            EntityGuid = entityGuid;
            P1 = playerSpawnPosition;
        }
    }
}
