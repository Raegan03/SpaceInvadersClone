using JGajewski.Commands;

namespace JGajewski.Game.States.Gameplay.Commands
{
    public struct FinishGameplayCommand : ICommand
    {
    }

    public class FinishGameplayCommandHandler : CommandHandler<FinishGameplayCommand>
    {
        
        
        protected override void ExplicitExecute(FinishGameplayCommand command)
        {
        }
    }
}
