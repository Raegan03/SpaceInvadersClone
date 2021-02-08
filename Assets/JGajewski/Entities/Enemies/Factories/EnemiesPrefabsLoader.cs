using Cysharp.Threading.Tasks;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Views;
using JGajewski.Entities.Interfaces.Factories;
using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace JGajewski.Entities.Enemies.Factories
{
    public class EnemiesPrefabsLoader : IEntityPrefabProvider<EnemyView>, IEntityPrefabProvider<EnemiesWaveView>, ILoadingStateAction
    {
        public int Priority => 0;
        
        private EnemyView _loadedEnemiesView;
        private readonly EnemiesSettings _enemiesSettings;

        [Inject]
        public EnemiesPrefabsLoader(EnemiesSettings enemiesSettings)
        {
            _enemiesSettings = enemiesSettings;
        }

        public async UniTask EnterAction(IState currentState)
        {
            var loadingTask = Addressables
                .LoadAssetAsync<GameObject>(_enemiesSettings.EnemyPrefabReference);
            
            await loadingTask;

            if (!loadingTask.IsDone || loadingTask.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Loading enemy prefab error!");
                return;
            }

            if (!loadingTask.Result.TryGetComponent(out _loadedEnemiesView))
            {
                Debug.LogError("Enemy prefab needs to have EnemyView component on it!");
            }
        }

        public EnemyView GetPrefab() => _loadedEnemiesView;

        EnemiesWaveView IEntityPrefabProvider<EnemiesWaveView>.GetPrefab() => _enemiesSettings.EnemiesWaveView;
    }
}
