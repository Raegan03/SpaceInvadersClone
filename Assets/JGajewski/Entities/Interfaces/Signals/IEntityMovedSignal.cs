using UnityEngine;

namespace JGajewski.Entities.Interfaces.Signals
{
    public interface IEntityMovedSignal : IEntitySignal
    {
        Vector3 EntityPosition { get; }
    }
}
