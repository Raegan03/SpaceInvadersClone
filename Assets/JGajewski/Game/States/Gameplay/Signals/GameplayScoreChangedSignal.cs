namespace JGajewski.Game.States.Gameplay.Signals
{
    public class GameplayScoreChangedSignal
    {
        public readonly int Score;

        public GameplayScoreChangedSignal(int score)
        {
            Score = score;
        }
    }
}
