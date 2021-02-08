using System;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Signals
{
    public class EnemiesWaveCreatedSignal : IEntityCreatedSignal<Vector3>
    {
        public Guid EntityGuid { get; }

        public Vector3 P1 { get; }
        
        public EnemiesWaveCreatedSignal(Guid entityGuid, Vector3 wavePosition)
        {
            EntityGuid = entityGuid;
            P1 = wavePosition;
        }
    }
}
