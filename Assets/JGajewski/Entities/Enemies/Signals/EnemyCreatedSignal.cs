using System;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Signals
{
    public class EnemyCreatedSignal : IEntityCreatedSignal<Vector3, EnemyType>
    {
        public Guid EntityGuid { get; }
        
        public Vector3 P1 { get; }
        public EnemyType P2 { get; }

        public EnemyCreatedSignal(Guid entityGuid, Vector3 entityPosition, EnemyType enemyType)
        {
            EntityGuid = entityGuid;
            P1 = entityPosition;
            P2 = enemyType;
        }
    }
}
