using JGajewski.Commands;
using JGajewski.Game.States.Loading.Commands;
using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.Game.States.Loading.Views;
using JGajewski.StateMachine.Abstracts;

namespace JGajewski.Game.States.Loading
{
    public class LoadingStateInstaller : ActionStateInstaller<LoadingState, 
        ILoadingStateAction, LoadingStateView>
    {
        public override void InstallBindings()
        {
            base.InstallBindings();            
            Container.BindCommandHandler<LoadingProceedCommand, LoadingProceedCommandHandler>();
        }
    }
}
