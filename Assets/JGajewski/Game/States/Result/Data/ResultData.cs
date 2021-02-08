using System;

namespace JGajewski.Game.States.Result.Data
{
    public class ResultData
    {
        public int Score { get; private set; }
        public int WeavesCleared { get; private set; }

        public DateTime ScoreAcquiredTime { get; private set; }
        
        public ResultData(int score, int weavesCleared, DateTime scoreAcquiredTime)
        {
            Score = score;
            WeavesCleared = weavesCleared;

            ScoreAcquiredTime = scoreAcquiredTime;
        }
    }
}
