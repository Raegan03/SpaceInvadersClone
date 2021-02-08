using System;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Signals
{
    public class EnemiesWaveMovedSignal : IEntityMovedSignal
    {
        public Guid EntityGuid { get; }
        public Vector3 EntityPosition { get; }

        public EnemiesWaveMovedSignal(Guid entityGuid, Vector3 entityPosition)
        {
            EntityGuid = entityGuid;
            EntityPosition = entityPosition;
        }
    }
}
