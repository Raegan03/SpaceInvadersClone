using Cysharp.Threading.Tasks;
using JGajewski.StateMachine.Interfaces;
using Zenject;

namespace JGajewski.StateMachine
{
    public static class StateMachineExtensions
    {
        public static void BindActionInstance<TAction, TInstance>(this DiContainer container, TInstance instance)
            where TAction : IStateAction
            where TInstance : TAction
        {
            container.Bind<TAction>()
                .To<TInstance>()
                .FromInstance(instance)
                .AsSingle();
        }

        public static void ChangeStateAndForget(this IStateMachine stateMachine, IState state)
        {
            stateMachine.ChangeState(state).Forget();
        }
    }
}
