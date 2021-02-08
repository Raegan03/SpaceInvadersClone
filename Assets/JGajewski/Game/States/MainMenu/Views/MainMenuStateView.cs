using JGajewski.Game.States.MainMenu.Interfaces;
using JGajewski.StateMachine.Abstracts.Views;

namespace JGajewski.Game.States.MainMenu.Views
{
    public class MainMenuStateView : StateView, IMainMenuStateAction
    {
        public override int Priority { get; } = -1;
    }
}
