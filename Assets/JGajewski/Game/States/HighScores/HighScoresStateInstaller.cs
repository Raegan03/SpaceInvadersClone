using JGajewski.Game.States.HighScores.Factories;
using JGajewski.Game.States.HighScores.Interfaces;
using JGajewski.Game.States.HighScores.Signals;
using JGajewski.Game.States.HighScores.Views;
using JGajewski.StateMachine.Abstracts;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.HighScores
{
    public class HighScoresStateInstaller : ActionStateInstaller<HighScoresState, 
        IHighScoresStateAction, HighScoresStateView>
    {
        [SerializeField] private HighScoreItemView highScoreItemView;
        [SerializeField] private Transform highScoreItemViewPoolRoot;
        
        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindMemoryPool<HighScoreItemView, HighScoresItemViewPool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(highScoreItemView)
                .UnderTransform(highScoreItemViewPoolRoot);

            Container.DeclareSignal<HighScoresDatabaseFetchedSignal>();
        }
    }
}
