using System;
using JGajewski.Commands;
using JGajewski.Game.States;
using JGajewski.Game.States.Result;
using JGajewski.StateMachine;
using Zenject;

namespace JGajewski.Game.Commands
{
    public struct SubmitGameplayResultCommand : ICommand
    {
        public readonly int Score;
        public readonly int WavesCleared;

        public SubmitGameplayResultCommand(int score, int wavesCleared)
        {
            Score = score;
            WavesCleared = wavesCleared;
        }
    }

    public class SubmitGameplayResultCommandHandler : CommandHandler<SubmitGameplayResultCommand>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ResultState _resultState;
        
        [Inject]
        public SubmitGameplayResultCommandHandler(GameStateMachine gameStateMachine, ResultState resultState)
        {
            _gameStateMachine = gameStateMachine;
            _resultState = resultState;
        }
        
        protected override void ExplicitExecute(SubmitGameplayResultCommand command)
        {
            if(_gameStateMachine.State == _resultState) return;
            
            _resultState.PopulateResult(command.Score, command.WavesCleared, DateTime.Now);
            _gameStateMachine.ChangeStateAndForget(_resultState);
        }
    }
}
