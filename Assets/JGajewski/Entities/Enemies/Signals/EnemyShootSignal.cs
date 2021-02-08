using System;
using JGajewski.Entities.Interfaces.Signals;

namespace JGajewski.Entities.Enemies.Signals
{
    public class EnemyShootSignal : IEntityShootSignal
    {
        public Guid EntityGuid { get; }
        public float ShootSpeed { get; }

        public EnemyShootSignal(Guid entityGuid, float shootSpeed)
        {
            EntityGuid = entityGuid;
            ShootSpeed = shootSpeed;
        }
    }
}
