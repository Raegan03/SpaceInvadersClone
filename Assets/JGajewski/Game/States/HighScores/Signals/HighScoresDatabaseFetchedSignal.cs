using System.Collections.Generic;
using JGajewski.Game.Database.Data;

namespace JGajewski.Game.States.HighScores.Signals
{
    public class HighScoresDatabaseFetchedSignal
    {
        public readonly IReadOnlyList<HighScoreEntry> HighScoreEntities;

        public HighScoresDatabaseFetchedSignal(IReadOnlyList<HighScoreEntry> highScoreEntities)
        {
            HighScoreEntities = highScoreEntities;
        }
    }
}
