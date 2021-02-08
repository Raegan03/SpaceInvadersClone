using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.StateMachine.Abstracts.Views;

namespace JGajewski.Game.States.Loading.Views
{
    public class LoadingStateView : StateView, ILoadingStateAction
    {
        public override int Priority { get; } = -1;
    }
}
