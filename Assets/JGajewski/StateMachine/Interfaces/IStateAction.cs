using Cysharp.Threading.Tasks;

namespace JGajewski.StateMachine.Interfaces
{
    public interface IStateAction
    {
        int Priority { get; }
    }

    public interface IStatePreEnterAction
    {
        void PreEnterAction(IState currentState);
    }
    
    public interface IStateEnterAction
    {
        UniTask EnterAction(IState currentState);
    }
    
    public interface IStatePostEnterAction
    {
        void PostEnterAction(IState currentState);
    }
    
    public interface IStatePreExitAction
    {
        void PreExitAction(IState currentState);
    }
    
    public interface IStateExitAction
    {
        UniTask ExitAction(IState currentState);
    }
    
    public interface IStatePostExitAction
    {
        void PostExitAction(IState currentState);
    }
}
