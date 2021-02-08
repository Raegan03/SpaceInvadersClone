using Cysharp.Threading.Tasks;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;

namespace JGajewski.StateMachine
{
    public abstract class StateMachine : IStateMachine
    {
        public IState State { get; protected set; }
        
        protected bool InTransition { get; set; }
        
        public async UniTask ChangeState(IState newState)
        {
            if (InTransition)
            {
                Debug.LogError("Can't change state when state machine is in transition!");
                return;
            }
            
            if (newState == null)
            {
                Debug.LogError("Can't change to null state!");
                return;
            }
            
            if (State == newState)
            {
                Debug.LogError("Can't change to same state!");
                return;
            }

            State?.PreExit();
            InTransition = true;

            if (State != null)
            {
                Debug.Log($"[ {GetType()} ] Exiting {State.GetType()} state");
                await State.OnExit();
                State.PostExit();
            }

            State = newState;

            Debug.Log($"[ {GetType()} ] Entering {State.GetType()} state");
            State.PreEnter();
            await State.OnEnter();
            
            InTransition = false;
            State.PostEnter();
        }
    }
}
