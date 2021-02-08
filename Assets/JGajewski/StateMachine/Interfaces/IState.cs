using Cysharp.Threading.Tasks;

namespace JGajewski.StateMachine.Interfaces
{
    public interface IState
    {
        bool IsActive { get; }
        
        void PreEnter();
        UniTask OnEnter();
        void PostEnter();
        
        void PreExit();
        UniTask OnExit();
        void PostExit();
    }
}
