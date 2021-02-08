using System;
using JGajewski.Entities.Interfaces.Signals;

namespace JGajewski.Entities.Player.Signals
{
    public class PlayerShootSignal : IEntityShootSignal
    {
        public Guid EntityGuid { get; }
        public float ShootSpeed { get; }

        public PlayerShootSignal(Guid entityGuid, float shootPower)
        {
            EntityGuid = entityGuid;
            ShootSpeed = shootPower;
        }
    }
}
