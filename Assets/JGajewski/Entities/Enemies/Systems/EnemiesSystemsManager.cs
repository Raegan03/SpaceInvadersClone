using System;
using System.Collections.Generic;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Entities.Interfaces.Systems;
using JGajewski.Game.Data;
using JGajewski.Game.States.Gameplay.Interfaces;
using JGajewski.StateMachine.Interfaces;
using Zenject;

namespace JGajewski.Entities.Enemies.Systems
{
    public class EnemiesSystemsManager : EntitySystemsManager<EnemiesSystemsManager, EnemiesWaveEntity>, 
        IGameplayStateAction, IStatePreEnterAction, IStatePostEnterAction, IStatePreExitAction, IStatePostExitAction
    {
        public int Priority => 0;
        
        private readonly EnemiesSettings _enemiesSettings;
        private readonly GameSettings _gameSettings;
        
        private EnemiesWaveEntity _enemiesWaveEntity;
        
        [Inject]
        public EnemiesSystemsManager(SignalBus signalBus, EnemiesSettings enemiesSettings, 
            GameSettings gameSettings, List<IEntitySystem<EnemiesSystemsManager, EnemiesWaveEntity>> subControllers) 
            : base(signalBus, subControllers)
        {
            _enemiesSettings = enemiesSettings;
            _gameSettings = gameSettings;
        }

        public void PreEnterAction(IState currentState)
        {
            _enemiesWaveEntity = new EnemiesWaveEntity(_enemiesSettings, _gameSettings);

            SignalBus.Fire(new EnemiesWaveCreatedSignal(
                _enemiesWaveEntity.EntityGuid, _enemiesWaveEntity.WaveCombinedPosition));
        }

        public void PostEnterAction(IState currentState)
        {
            StartSystems(_enemiesWaveEntity);
            _enemiesWaveEntity.CreateWave();
        }

        public void PreExitAction(IState currentState)
        {
            _enemiesWaveEntity.ClearWave();
            StopSystems();
        }

        public void PostExitAction(IState currentState)
        {
            SignalBus.Fire(new EnemiesWaveDestroyedSignal(_enemiesWaveEntity.EntityGuid));
            _enemiesWaveEntity = null;
        }

        public void DestroyEnemyEntity(Guid entityGuid)
        {
            _enemiesWaveEntity.DestroyEnemyEntity(entityGuid);
        }
    }
}
