namespace JGajewski.Entities.Player.Signals
{
    public class PlayerDamagedSignal
    {
        public readonly int CurrentPlayerLives;
        
        public PlayerDamagedSignal(int currentPlayerLives)
        {
            CurrentPlayerLives = currentPlayerLives;
        }
    }
}
