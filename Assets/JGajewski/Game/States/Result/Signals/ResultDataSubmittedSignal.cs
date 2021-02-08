namespace JGajewski.Game.States.Result.Signals
{
    public class ResultDataSubmittedSignal
    {
        public readonly int Score;
        public readonly int WavesCleared;

        public ResultDataSubmittedSignal(int score, int wavesCleared)
        {
            Score = score;
            WavesCleared = wavesCleared;
        }
    }
}
