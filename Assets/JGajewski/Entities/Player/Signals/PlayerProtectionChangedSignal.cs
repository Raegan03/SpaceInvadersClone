namespace JGajewski.Entities.Player.Signals
{
    public class PlayerProtectionChangedSignal
    {
        public readonly bool ProtectionStatus;
        
        public PlayerProtectionChangedSignal(bool protectionStatus)
        {
            ProtectionStatus = protectionStatus;
        }
    }
}
