using Cysharp.Threading.Tasks;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;

namespace JGajewski.StateMachine.Abstracts.Views
{
    public abstract class StateView : MonoBehaviour, IStateAction, 
        IStatePreEnterAction, IStateEnterAction, IStatePostEnterAction, 
        IStatePreExitAction, IStateExitAction, IStatePostExitAction
    {
        public abstract int Priority { get; }
        
        [SerializeField] private GameObject stateRoot;
        [SerializeField] private StateViewComponent[] stateViewComponents;

        public virtual void PreEnterAction(IState currentState)
        {
            var stateViewComponentsLength = stateViewComponents.Length;
            for (int i = 0; i < stateViewComponentsLength; i++)
            {
                stateViewComponents[i].Populate(this);
            }
            
            stateRoot.SetActive(true);
        }

        public virtual async UniTask EnterAction(IState currentState)
        {
            var stateViewComponentsLength = stateViewComponents.Length;
            for (int i = 0; i < stateViewComponentsLength; i++)
            {
                await stateViewComponents[i].Enter();
            }
        }
        
        public virtual void PostEnterAction(IState currentState)
        {
        }

        public virtual void PreExitAction(IState currentState)
        {
        }

        public virtual async UniTask ExitAction(IState currentState)
        {
            var stateViewComponentsLength = stateViewComponents.Length;
            for (int i = 0; i < stateViewComponentsLength; i++)
            {
                await stateViewComponents[i].Exit();
            }
        }

        public virtual void PostExitAction(IState currentState)
        {
            stateRoot.SetActive(false);

            var stateViewComponentsLength = stateViewComponents.Length;
            for (int i = 0; i < stateViewComponentsLength; i++)
            {
                stateViewComponents[i].Clear();
            }
        }

        public abstract class StateViewComponent : MonoBehaviour
        {
            protected StateView StateView;

            public virtual void Populate(StateView stateView)
            {
                StateView = stateView;
            }

            public abstract UniTask Enter();
            public abstract UniTask Exit();

            public virtual void Clear()
            {
                StateView = null;
            }
        }
    }
}
