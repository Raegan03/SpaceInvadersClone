using System.Collections.Generic;
using JGajewski.Game.States.MainMenu.Interfaces;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.MainMenu
{
    public class MainMenuState : ActionState<IMainMenuStateAction>
    {
        [Inject]
        public MainMenuState(List<IMainMenuStateAction> stateActions) 
            : base(stateActions)
        {
        }
    }
}
