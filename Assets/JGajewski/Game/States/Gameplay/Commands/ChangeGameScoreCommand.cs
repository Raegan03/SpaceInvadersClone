using JGajewski.Commands;
using Zenject;

namespace JGajewski.Game.States.Gameplay.Commands
{
    public struct ChangeGameScoreCommand : ICommand
    {
        public readonly int ScoreChange;

        public ChangeGameScoreCommand(int scoreChange)
        {
            ScoreChange = scoreChange;
        }
    }

    public class AddGameScoreCommandHandler : CommandHandler<ChangeGameScoreCommand>
    {
        private readonly GameplayState _gameplayState;
        
        [Inject]
        public AddGameScoreCommandHandler(GameplayState gameplayState)
        {
            _gameplayState = gameplayState;
        }
        
        protected override void ExplicitExecute(ChangeGameScoreCommand command)
        {
            if(!_gameplayState.IsActive) return;
            _gameplayState.ChangeGameplayScore(command.ScoreChange);
        }
    }
}
