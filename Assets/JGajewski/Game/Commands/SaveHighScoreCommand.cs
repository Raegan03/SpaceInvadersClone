using System;
using JGajewski.Commands;
using JGajewski.Game.Database;

namespace JGajewski.Game.Commands
{
    public struct SaveHighScoreCommand : ICommand
    {
        public readonly int Score;
        public readonly DateTime ScoreAcquiredTime;

        public SaveHighScoreCommand(int score, DateTime scoreAcquiredTime)
        {
            Score = score;
            ScoreAcquiredTime = scoreAcquiredTime;
        }
    }

    public class SaveHighScoreCommandHandler : CommandHandler<SaveHighScoreCommand>
    {
        private readonly HighScoresDatabase _highScoresDatabase;
        
        public SaveHighScoreCommandHandler(HighScoresDatabase highScoresDatabase)
        {
            _highScoresDatabase = highScoresDatabase;
        }
        
        protected override void ExplicitExecute(SaveHighScoreCommand command)
        {
            _highScoresDatabase.AddNewHighScore(command.Score, command.ScoreAcquiredTime);
        }
    }
}
