#if UNITY_ANDROID && !UNITY_EDITOR
using JGajewski.Common;
#endif
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Player.Signals;
using JGajewski.Game.Data;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Player.Systems
{
    public class PlayerMoveSystem : EntityUpdateSystem<PlayerSystemsManager, PlayerEntity>
    {
        private GameSettings _gameSettings;
        
        [Inject]
        public PlayerMoveSystem(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public override void Start(PlayerSystemsManager manager, PlayerEntity data)
        {
            base.Start(manager, data);
            Data.OnPlayerMoved += HandlePlayerMoved;
        }

        public override void Stop()
        {
            Data.OnPlayerMoved -= HandlePlayerMoved;
            base.Stop();
        }

        public override void Update()
        {
            base.Update();
            
#if !UNITY_ANDROID || UNITY_EDITOR
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Data.UpdatePlayerPosition(1f, _gameSettings.HorizontalGameViewRange);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Data.UpdatePlayerPosition(-1f, _gameSettings.HorizontalGameViewRange);
            }
            
#else

            if (Input.touchCount <= 0) return;

            var touch = Input.GetTouch(0);
            var normalizedTouchPosition = touch.position.x.Remap(0f, Screen.width, 1f, 0f);
            Data.SetPlayerPosition(_gameSettings.HorizontalGameViewRange.Lerp(normalizedTouchPosition), _gameSettings.HorizontalGameViewRange);

#endif
        }
        
        private void HandlePlayerMoved(Vector3 playerPosition)
        {
            Manager.SignalBus.Fire(new PlayerMovedSignal(Data.EntityGuid, playerPosition));
        }
    }
}
