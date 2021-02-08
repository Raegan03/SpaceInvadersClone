namespace JGajewski.Entities.Interfaces.Systems
{
    public interface IEntitySystem<TEntitySystemsManager, TData>
        where TEntitySystemsManager : IEntitySystemsManager<TEntitySystemsManager, TData>
    {
        TEntitySystemsManager Manager { get; }
        TData Data { get; }
        
        void Start(TEntitySystemsManager manager, TData data);
        void Stop();

        void Pause();
        void Resume();
    }
}
