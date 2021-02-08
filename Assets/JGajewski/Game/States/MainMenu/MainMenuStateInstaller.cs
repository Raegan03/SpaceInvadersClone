using JGajewski.Commands;
using JGajewski.Game.States.MainMenu.Commands;
using JGajewski.Game.States.MainMenu.Interfaces;
using JGajewski.Game.States.MainMenu.Views;
using JGajewski.StateMachine.Abstracts;

namespace JGajewski.Game.States.MainMenu
{
    public class MainMenuStateInstaller : ActionStateInstaller<MainMenuState, 
        IMainMenuStateAction, MainMenuStateView>
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindCommandHandler<StartGameCommand, StartGameCommandHandler>();
            Container.BindCommandHandler<OpenHighScoresCommand, OpenHighScoresCommandHandler>();
        }
    }
}
