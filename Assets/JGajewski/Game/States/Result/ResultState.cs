using System;
using System.Collections.Generic;
using JGajewski.Commands;
using JGajewski.Game.Commands;
using JGajewski.Game.States.Result.Data;
using JGajewski.Game.States.Result.Interfaces;
using JGajewski.Game.States.Result.Signals;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.Result
{
    public class ResultState : ActionState<IResultStateAction>
    {
        private readonly SignalBus _signalBus;
        private readonly CommandBus _commandBus;
        
        private ResultData _resultData;
        
        [Inject]
        public ResultState(SignalBus signalBus, CommandBus commandBus, List<IResultStateAction> stateActions) : base(stateActions)
        {
            _signalBus = signalBus;
            _commandBus = commandBus;
        }

        public void PopulateResult(int score, int weavesCleared, DateTime scoreAcquiredTime)
        {
            _resultData = new ResultData(score, weavesCleared, scoreAcquiredTime);
        }

        public override void PreEnter()
        {
            base.PreEnter();
            _signalBus.Fire(new ResultDataSubmittedSignal(_resultData.Score, _resultData.WeavesCleared));
        }

        public override void PostExit()
        {
            base.PostExit();
            _commandBus.Send(new SaveHighScoreCommand(_resultData.Score, _resultData.ScoreAcquiredTime));

            _resultData = null;
        }
    }
}
