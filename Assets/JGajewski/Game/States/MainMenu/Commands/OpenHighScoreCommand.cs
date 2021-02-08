using JGajewski.Commands;
using JGajewski.Game.States.HighScores;
using JGajewski.StateMachine;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.MainMenu.Commands
{
    public struct OpenHighScoresCommand : ICommand
    {
    }

    public class OpenHighScoresCommandHandler : CommandHandler<OpenHighScoresCommand>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly HighScoresState _highScoresState;

        [Inject]
        public OpenHighScoresCommandHandler(GameStateMachine gameStateMachine, HighScoresState highScoresState)
        {
            _gameStateMachine = gameStateMachine;
            _highScoresState = highScoresState;
        }
        
        protected override void ExplicitExecute(OpenHighScoresCommand command)
        {
            if(_gameStateMachine.State == _highScoresState) return;
            _gameStateMachine.ChangeStateAndForget(_highScoresState);
            Debug.Log("Open high scores!");
        }
    }
}