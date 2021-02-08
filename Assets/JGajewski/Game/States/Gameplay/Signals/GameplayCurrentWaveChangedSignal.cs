namespace JGajewski.Game.States.Gameplay.Signals
{
    public class GameplayCurrentWaveChangedSignal
    {
        public readonly int Waves;

        public GameplayCurrentWaveChangedSignal(int waves)
        {
            Waves = waves;
        }
    }
}
