using Cysharp.Threading.Tasks;

namespace JGajewski.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        IState State { get; }
        
        UniTask ChangeState(IState newState);
    }
}
