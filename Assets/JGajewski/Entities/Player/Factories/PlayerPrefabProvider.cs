using Cysharp.Threading.Tasks;
using JGajewski.Entities.Interfaces.Factories;
using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Player.Views;
using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace JGajewski.Entities.Player.Factories
{
    public class PlayerPrefabProvider : IEntityPrefabProvider<PlayerView>, ILoadingStateAction
    {
        public int Priority => 0;
        
        private PlayerView _loadedPlayerView;
        private readonly PlayerSettings _playerSettings;

        [Inject]
        public PlayerPrefabProvider(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
        }

        public async UniTask EnterAction(IState currentState)
        {
            var loadingTask = Addressables
                .LoadAssetAsync<GameObject>(_playerSettings.PlayerPrefabReference);

            await loadingTask;

            if (!loadingTask.IsDone || loadingTask.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Loading player prefab error!");
                return;
            }

            if (!loadingTask.Result.TryGetComponent(out _loadedPlayerView))
            {
                Debug.LogError("Player prefab needs to have PlayerView component on it!");
            }
        }

        public PlayerView GetPrefab() => _loadedPlayerView;
    }
}
