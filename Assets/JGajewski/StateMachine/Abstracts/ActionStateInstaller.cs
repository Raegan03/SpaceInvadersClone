using JGajewski.StateMachine.Abstracts.Views;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace JGajewski.StateMachine.Abstracts
{
    public abstract class ActionStateInstaller<TState, TStateAction, TStateView> : MonoInstaller
        where TStateAction : IStateAction
        where TState : ActionState<TStateAction>
        where TStateView : StateView, TStateAction
    {
        [SerializeField] private TStateView stateView;
        
        public override void InstallBindings()
        {
            Container.Bind<TState>()
                .AsSingle();
            
            Container.BindActionInstance<TStateAction, TStateView>(stateView);
        }
    }
}
