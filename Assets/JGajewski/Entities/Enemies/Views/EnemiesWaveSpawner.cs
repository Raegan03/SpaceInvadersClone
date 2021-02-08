using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Enemies.Factories;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Views
{
    public class EnemiesWaveSpawner : EntityViewComponent
    {
        [SerializeField] private EnemiesMonoPoolSpawner enemiesMonoPoolSpawner;
        
        protected override void OnPopulated()
        {
            enemiesMonoPoolSpawner.enabled = true;
        }

        protected override void OnCleared()
        {
            enemiesMonoPoolSpawner.enabled = false;
        }
    }
}
