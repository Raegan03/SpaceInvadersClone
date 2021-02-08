using System.Collections.Generic;
using JGajewski.Game.Data;
using JGajewski.Game.States.HighScores.Factories;
using JGajewski.Game.States.HighScores.Interfaces;
using JGajewski.Game.States.HighScores.Signals;
using JGajewski.StateMachine.Abstracts.Views;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.HighScores.Views
{
    public class HighScoresStateView : StateView, IHighScoresStateAction
    {
        public override int Priority { get; } = -1;

        [SerializeField] private Transform highScoreItemsRoot;

        private SignalBus _signalBus;
        private HighScoresItemViewPool _highScoresItemViewPool;
        private GameSettings _gameSettings;

        private List<HighScoreItemView> _highScoreItems;
        
        [Inject]
        private void Construct(SignalBus signalBus, HighScoresItemViewPool highScoresItemViewPool, GameSettings gameSettings)
        {
            _signalBus = signalBus;
            _highScoresItemViewPool = highScoresItemViewPool;
            _gameSettings = gameSettings;
        }

        public override void PreEnterAction(IState currentState)
        {
            base.PreEnterAction(currentState);
            _signalBus.Subscribe<HighScoresDatabaseFetchedSignal>(HandleHighScoresDatabaseFetched);
        }

        public override void PostExitAction(IState currentState)
        {
            base.PostExitAction(currentState);
            _signalBus.Unsubscribe<HighScoresDatabaseFetchedSignal>(HandleHighScoresDatabaseFetched);
            
            var highScoreItemsCount = _highScoreItems.Count;
            for (int i = 0; i < highScoreItemsCount; i++)
            {
                _highScoresItemViewPool.Despawn(_highScoreItems[i]);
            }
            _highScoreItems.Clear();
        }

        private void HandleHighScoresDatabaseFetched(HighScoresDatabaseFetchedSignal s)
        {
            var highScoresCount = Mathf.Min(_gameSettings.HighScoresCountOnList, s.HighScoreEntities.Count);
            
            _highScoreItems = new List<HighScoreItemView>(highScoresCount);
            for (int i = 0; i < highScoresCount; i++)
            {
                var highScoreEntity = s.HighScoreEntities[i];
                var highScoreItem = _highScoresItemViewPool.Spawn(highScoreItemsRoot, i + 1, highScoreEntity.ScoreAcquiredDateTime,
                    highScoreEntity.Score);
                
                _highScoreItems.Add(highScoreItem);
            }
        }
    }
}
