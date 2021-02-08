using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JGajewski.Commands;
using JGajewski.Game.Commands;
using JGajewski.Game.States.Gameplay.Data;
using JGajewski.Game.States.Gameplay.Interfaces;
using JGajewski.Game.States.Gameplay.Signals;
using JGajewski.StateMachine.Abstracts;
using Zenject;

namespace JGajewski.Game.States.Gameplay
{
    public class GameplayState : ActionState<IGameplayStateAction>
    {
        private readonly SignalBus _signalBus;
        private readonly CommandBus _commandBus;
        
        private GameplayScore _gameplayScore;
        
        [Inject]
        public GameplayState(SignalBus signalBus, CommandBus commandBus, List<IGameplayStateAction> stateActions) 
            : base(stateActions)
        {
            _signalBus = signalBus;
            _commandBus = commandBus;
        }

        public override async UniTask OnEnter()
        {
            _signalBus.Subscribe<IGameplayFinishedSignal>(HandleGameplayFinished);
            
            _gameplayScore = new GameplayScore();
            _gameplayScore.OnScoreChanged += OnGameplayScoreChanged;
            _gameplayScore.OnCurrentWaveChanged += OnCurrentWaveChanged;
            
            await base.OnEnter();
            
            OnGameplayScoreChanged(_gameplayScore.Score);
            OnCurrentWaveChanged(_gameplayScore.CurrentWave);
        }

        public override void PostExit()
        {
            _signalBus.Unsubscribe<IGameplayFinishedSignal>(HandleGameplayFinished);
            
            _gameplayScore.OnScoreChanged -= OnGameplayScoreChanged;
            _gameplayScore.OnCurrentWaveChanged -= OnCurrentWaveChanged;
            
            _gameplayScore = null;
            
            base.PostExit();
        }
        
        public void SubmitWaveCleared()
        {
            _gameplayScore?.WaveCleared();
        }

        public void ChangeGameplayScore(int change)
        {
            _gameplayScore?.ChangeScore(change);
        }

        private void OnGameplayScoreChanged(int score)
        {
            _signalBus.Fire(new GameplayScoreChangedSignal(score));
        }
        
        private void OnCurrentWaveChanged(int wave)
        {
            _signalBus.Fire(new GameplayCurrentWaveChangedSignal(wave));
        }

        private void HandleGameplayFinished(IGameplayFinishedSignal _)
        {
            _commandBus.Send(new SubmitGameplayResultCommand(_gameplayScore.Score, _gameplayScore.WavesCleared));
        }
    }
}
