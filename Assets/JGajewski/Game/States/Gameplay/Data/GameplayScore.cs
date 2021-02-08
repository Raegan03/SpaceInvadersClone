using UnityEngine.Events;

namespace JGajewski.Game.States.Gameplay.Data
{
    public class GameplayScore
    {
        public event UnityAction<int> OnCurrentWaveChanged;
        public event UnityAction<int> OnScoreChanged;
        
        public int Score { get; private set; }
        public int WavesCleared { get; private set; }
        
        public int CurrentWave => WavesCleared + 1;

        public GameplayScore()
        {
            Score = 0;
            WavesCleared = 0;
        }
        
        public void WaveCleared()
        {
            WavesCleared++;
            OnCurrentWaveChanged?.Invoke(CurrentWave);
        }

        public void ChangeScore(int change)
        {
            Score = Score + change < 0 ? 0 : Score + change;
            OnScoreChanged?.Invoke(Score);
        }
    }
}
