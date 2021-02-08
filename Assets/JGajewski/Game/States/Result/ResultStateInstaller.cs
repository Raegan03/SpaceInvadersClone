using JGajewski.Game.States.Result.Interfaces;
using JGajewski.Game.States.Result.Signals;
using JGajewski.Game.States.Result.Views;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.Result
{
    public class ResultStateInstaller : ActionStateInstaller<ResultState, 
        IResultStateAction, ResultStateView>
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.DeclareSignal<ResultDataSubmittedSignal>();
        }
    }
}
