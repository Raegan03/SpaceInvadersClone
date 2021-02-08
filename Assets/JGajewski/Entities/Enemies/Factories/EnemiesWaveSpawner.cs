using JGajewski.Entities.Abstracts.Spawners;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Entities.Enemies.Views;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Factories
{
    public class EnemiesWaveSpawner : EntitySpawner<EnemiesWavePlaceholderFactory, 
        Vector3, EnemiesWaveView, EnemiesWaveCreatedSignal, EnemiesWaveDestroyedSignal>
    {
        protected override void HandleEntityCreated(EnemiesWaveCreatedSignal s)
        {
            base.HandleEntityCreated(s);
            if(!SpawnedViews.TryGetValue(s.EntityGuid, out var view)) return;

            var enemiesWaveView = (EnemiesWaveView)view;
            enemiesWaveView.SetupPosition(s.P1);
        }
    }
}
