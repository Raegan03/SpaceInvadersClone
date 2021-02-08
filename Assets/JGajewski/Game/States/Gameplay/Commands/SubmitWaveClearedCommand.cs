using JGajewski.Commands;

namespace JGajewski.Game.States.Gameplay.Commands
{
    public struct SubmitWaveClearedCommand : ICommand
    {
    }

    public class SubmitWaveClearedCommandHandler : CommandHandler<SubmitWaveClearedCommand>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameplayState _gameplayState;
        
        public SubmitWaveClearedCommandHandler(GameStateMachine gameStateMachine, GameplayState gameplayState)
        {
            _gameStateMachine = gameStateMachine;
            _gameplayState = gameplayState;
        }
        
        protected override void ExplicitExecute(SubmitWaveClearedCommand command)
        {
            if(_gameStateMachine.State != _gameplayState) return;
            _gameplayState.SubmitWaveCleared();
        }
    }
}
