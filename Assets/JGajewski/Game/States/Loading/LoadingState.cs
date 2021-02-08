using System.Collections.Generic;
using JGajewski.Commands;
using JGajewski.Game.States.Loading.Commands;
using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.Loading
{
    public class LoadingState : ActionState<ILoadingStateAction>
    {
        private readonly CommandBus _commandBus;
        private readonly LoadingProceedCommand _loadingProceedCommand = new LoadingProceedCommand();
        
        [Inject]
        public LoadingState(CommandBus commandBus, List<ILoadingStateAction> stateActions) 
            : base(stateActions)
        {
            _commandBus = commandBus;
        }
        
        public override void PostEnter()
        {
            _commandBus.Send(_loadingProceedCommand);
        }
    }
}
