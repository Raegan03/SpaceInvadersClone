using System;
using Newtonsoft.Json;

namespace JGajewski.Game.Database.Data
{
    [Serializable]
    public class HighScoreEntry
    {
        [JsonProperty("score")]
        public int Score { get; private set; }
        
        [JsonProperty("score_acquired_data_time")]
        public DateTime ScoreAcquiredDateTime { get; private set; }

        public HighScoreEntry(int score, DateTime scoreAcquiredDateTime)
        {
            Score = score;
            ScoreAcquiredDateTime = scoreAcquiredDateTime;
        }
    }
}
