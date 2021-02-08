namespace JGajewski.Entities.Interfaces.Signals
{
    public interface IEntityCreatedSignal : IEntitySignal
    {
    }
    
    public interface IEntityCreatedSignal<out T1> : IEntitySignal
    {
        T1 P1 { get; }
    }
    
    public interface IEntityCreatedSignal<out T1, out T2> : IEntitySignal
    {
        T1 P1 { get; }
        T2 P2 { get; }
    }
}
