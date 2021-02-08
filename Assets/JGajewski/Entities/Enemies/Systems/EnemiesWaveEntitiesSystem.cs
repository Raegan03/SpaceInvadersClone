using System;
using JGajewski.Commands;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Game.States.Gameplay.Commands;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Systems
{
    public class EnemiesWaveEntitiesSystem : EntitySystem<EnemiesSystemsManager, EnemiesWaveEntity>
    {
        private readonly CommandBus _commandBus;
        
        public EnemiesWaveEntitiesSystem(CommandBus commandBus)
        {
            _commandBus = commandBus;
        }
        
        public override void Start(EnemiesSystemsManager manager, EnemiesWaveEntity data)
        {
            base.Start(manager, data);

            Data.OnEnemyEntityCreated += OnEnemyEntityCreated;
            Data.OnEnemyEntityDestroyed += OnEnemyEntityDestroyed;
        }

        public override void Stop()
        {
            Data.OnEnemyEntityCreated -= OnEnemyEntityCreated;
            Data.OnEnemyEntityDestroyed -= OnEnemyEntityDestroyed;
            
            base.Stop();
        }

        private void OnEnemyEntityCreated(Guid enemyGuid, Vector3 enemyPosition, EnemyType enemyType)
        {
            Manager.SignalBus.Fire(new EnemyCreatedSignal(enemyGuid, enemyPosition, enemyType));
        }
        
        private void OnEnemyEntityDestroyed(Guid enemyGuid, int score)
        {
            Manager.SignalBus.Fire(new EnemyDestroyedSignal(enemyGuid));
            if(score != 0) _commandBus.Send(new ChangeGameScoreCommand(score));
        }
    }
}
