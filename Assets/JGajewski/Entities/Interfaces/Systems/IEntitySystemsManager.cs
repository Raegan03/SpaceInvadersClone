using System.Collections.Generic;

namespace JGajewski.Entities.Interfaces.Systems
{
    public interface IEntitySystemsManager<TEntitySystemsManager, TData>
        where TEntitySystemsManager : IEntitySystemsManager<TEntitySystemsManager, TData>
    {
        List<IEntitySystem<TEntitySystemsManager, TData>> Systems { get; }
        bool IsEnabled { get; }
    }
}
