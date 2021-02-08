using Cysharp.Threading.Tasks;
using JGajewski.Entities.Interfaces.Factories;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Views;
using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.StateMachine.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace JGajewski.Entities.Projectile.Factories
{
    public class ProjectileEntityPrefabProvider : IEntityPrefabProvider<ProjectileView>, ILoadingStateAction
    {
        public int Priority { get; } = 0;

        private ProjectileView _projectileLoadedPrefab;
        private readonly ProjectileSettings _projectileSettings;

        [Inject]
        public ProjectileEntityPrefabProvider(ProjectileSettings projectileSettings)
        {
            _projectileSettings = projectileSettings;
        }

        public async UniTask EnterAction(IState currentState)
        {
            var loadingTask = Addressables
                .LoadAssetAsync<GameObject>(_projectileSettings.ProjectilePrefabReference);

            await loadingTask;

            if (!loadingTask.IsDone || loadingTask.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Loading player prefab error!");
                return;
            }

            var projectileObjectLoaded = loadingTask.Result;
            if (!projectileObjectLoaded.TryGetComponent(out _projectileLoadedPrefab))
            {
                Debug.LogError($"Can't get ProjectileView from prefab {projectileObjectLoaded.name}!");
            }
        }

        public ProjectileView GetPrefab() => _projectileLoadedPrefab;
    }
}
