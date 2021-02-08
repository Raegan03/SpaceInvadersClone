using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JGajewski.StateMachine.Interfaces;

namespace JGajewski.StateMachine.Abstracts
{
    public abstract class ActionState<TStateAction> : IState
        where TStateAction : IStateAction
    {
        public bool IsActive { get; protected set; }

        protected readonly List<TStateAction> StateActions;
        
        protected readonly List<IStatePreEnterAction> PreEnterActions;
        protected readonly List<IStateEnterAction> EnterActions;
        protected readonly List<IStatePostEnterAction> PostEnterActions;
        
        protected readonly List<IStatePreExitAction> PreExitActions;
        protected readonly List<IStateExitAction> ExitActions;
        protected readonly List<IStatePostExitAction> PostExitActions;
        
        protected ActionState(List<TStateAction> stateActions)
        {
            StateActions = stateActions
                .OrderBy(action => action.Priority)
                .ToList();

            PreEnterActions = new List<IStatePreEnterAction>();
            EnterActions = new List<IStateEnterAction>();
            PostEnterActions = new List<IStatePostEnterAction>();

            PreExitActions = new List<IStatePreExitAction>();
            ExitActions = new List<IStateExitAction>();
            PostExitActions = new List<IStatePostExitAction>();

            var stateActionsCount = StateActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                var stateAction = StateActions[i];

                if (stateAction is IStatePreEnterAction statePreEnterAction)
                {
                    PreEnterActions.Add(statePreEnterAction);
                }
                
                if (stateAction is IStateEnterAction stateEnterAction)
                {
                    EnterActions.Add(stateEnterAction);
                }
                
                if (stateAction is IStatePostEnterAction statePostEnterAction)
                {
                    PostEnterActions.Add(statePostEnterAction);
                }
                
                if (stateAction is IStatePreExitAction statePreExitAction)
                {
                    PreExitActions.Add(statePreExitAction);
                }
                
                if (stateAction is IStateExitAction stateExitAction)
                {
                    ExitActions.Add(stateExitAction);
                }
                
                if (stateAction is IStatePostExitAction statePostExitAction)
                {
                    PostExitActions.Add(statePostExitAction);
                }
            }
        }
        
        public virtual void PreEnter()
        {
            var stateActionsCount = PreEnterActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                PreEnterActions[i].PreEnterAction(this);
            }
        }

        public virtual async UniTask OnEnter()
        {
            var stateActionsCount = EnterActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                await EnterActions[i].EnterAction(this);
            }
        }

        public virtual void PostEnter()
        {
            IsActive = true;
            var stateActionsCount = PostEnterActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                PostEnterActions[i].PostEnterAction(this);
            }
        }
        
        public virtual void PreExit()
        {
            var stateActionsCount = PreExitActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                PreExitActions[i].PreExitAction(this);
            }
        }

        public virtual async UniTask OnExit()
        {
            var stateActionsCount = ExitActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                await ExitActions[i].ExitAction(this);
            }
        }

        public virtual void PostExit()
        {
            IsActive = false;
            var stateActionsCount = PostExitActions.Count;
            for (int i = 0; i < stateActionsCount; i++)
            {
                PostExitActions[i].PostExitAction(this);
            }
        }
    }
}
