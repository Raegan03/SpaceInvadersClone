using JGajewski.Commands;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Game.States.Gameplay.Commands;

namespace JGajewski.Entities.Enemies.Systems
{
    public class EnemiesWaveMaintainerSystem : EntitySystem<EnemiesSystemsManager, EnemiesWaveEntity>
    {
        private readonly CommandBus _commandBus;
        private readonly EnemiesSettings _enemiesSettings;
        
        public EnemiesWaveMaintainerSystem(CommandBus commandBus, EnemiesSettings enemiesSettings)
        {
            _commandBus = commandBus;
            _enemiesSettings = enemiesSettings;
        }
        
        public override void Start(EnemiesSystemsManager manager, EnemiesWaveEntity data)
        {
            base.Start(manager, data);

            Data.OnEnemiesWaveCleared += OnEnemiesWaveCleared;
            Data.OnEnemiesWaveMovedDown += OnEnemiesEnemiesWaveMovedDown;
        }

        public override void Stop()
        {
            Data.OnEnemiesWaveCleared -= OnEnemiesWaveCleared;
            Data.OnEnemiesWaveMovedDown -= OnEnemiesEnemiesWaveMovedDown;
            
            base.Stop();
        }
        
        private void OnEnemiesWaveCleared()
        {
            Data.ClearWave();
            _commandBus.Send(new SubmitWaveClearedCommand());
            
            Data.CreateWave();
        }

        private void OnEnemiesEnemiesWaveMovedDown(int lowestWaveVerticalIndex)
        {
            if (lowestWaveVerticalIndex > _enemiesSettings.LowestWaveVerticalIndex)
                Manager.SignalBus.AbstractFire<EnemiesWaveReachedPlayerSignal>();
        }
    }
}
