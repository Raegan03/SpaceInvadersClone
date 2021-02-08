using System.Collections.Generic;
using JGajewski.Game.Database;
using JGajewski.Game.States.HighScores.Interfaces;
using JGajewski.Game.States.HighScores.Signals;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.HighScores
{
    public class HighScoresState : ActionState<IHighScoresStateAction>
    {
        private readonly SignalBus _signalBus;
        private readonly HighScoresDatabase _highScoresDatabase;
        
        [Inject]
        public HighScoresState(SignalBus signalBus, HighScoresDatabase highScoresDatabase, List<IHighScoresStateAction> stateActions) 
            : base(stateActions)
        {
            _signalBus = signalBus;
            _highScoresDatabase = highScoresDatabase;
        }

        public override void PreEnter()
        {
            base.PreEnter();

            var highScoresEntities = _highScoresDatabase.HighScoreEntities;
            _signalBus.Fire(new HighScoresDatabaseFetchedSignal(highScoresEntities));
        }
    }
}
