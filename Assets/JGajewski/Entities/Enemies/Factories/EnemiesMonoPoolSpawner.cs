using JGajewski.Entities.Abstracts.Spawners;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Entities.Enemies.Views;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Factories
{
    public class EnemiesMonoPoolSpawner : EntityMonoPoolSpawner<Vector3, EnemyType, EnemiesMonoPool, 
        EnemyView, EnemyCreatedSignal, EnemyDestroyedSignal>
    {
    }
}
