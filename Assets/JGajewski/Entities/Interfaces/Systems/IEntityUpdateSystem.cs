namespace JGajewski.Entities.Interfaces.Systems
{
    public interface IEntityUpdateSystem<TEntitySystemsManager, TData> : IEntitySystem<TEntitySystemsManager, TData>
        where TEntitySystemsManager : IEntitySystemsManager<TEntitySystemsManager, TData>
    {
        void Update();
    }
}
