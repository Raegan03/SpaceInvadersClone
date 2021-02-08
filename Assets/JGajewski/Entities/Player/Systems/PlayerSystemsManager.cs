using System.Collections.Generic;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Interfaces.Systems;
using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Player.Signals;
using JGajewski.Game.Data;
using JGajewski.Game.States.Gameplay.Interfaces;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Player.Systems
{
    public class PlayerSystemsManager : EntitySystemsManager<PlayerSystemsManager, PlayerEntity>, IGameplayStateAction, 
        IStatePreEnterAction, IStatePostEnterAction, IStatePreExitAction, IStatePostExitAction
    {
        public int Priority { get; } = 0;

        private readonly PlayerSettings _playerSettings;
        private readonly GameSettings _gameSettings;

        private PlayerEntity _playerEntity;

        [Inject]
        public PlayerSystemsManager(SignalBus signalBus, PlayerSettings playerSettings, GameSettings gameSettings, 
            List<IEntitySystem<PlayerSystemsManager, PlayerEntity>> entitySystems) : base(signalBus, entitySystems)
        {
            _gameSettings = gameSettings;
            _playerSettings = playerSettings;
        }

        public void PreEnterAction(IState currentState)
        {
            _playerEntity = new PlayerEntity(_playerSettings, 
                new Vector3(0f, 0f, _gameSettings.VerticalGameViewRange.max));

            SignalBus.Fire(new PlayerCreatedSignal(_playerEntity.EntityGuid, _playerEntity.PlayerPosition));
        }

        public void PostEnterAction(IState currentState)
        {
            StartSystems(_playerEntity);
        }

        public void PreExitAction(IState currentState)
        {
            StopSystems();
        }

        public void PostExitAction(IState currentState)
        {
            SignalBus.Fire(new PlayerDestroyedSignal(_playerEntity.EntityGuid));
            _playerEntity = null;
        }

        public void DamagePlayer()
        {
            _playerEntity.TakeDamage();
        }
    }
}
