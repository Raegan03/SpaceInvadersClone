using System;
using System.Collections.Generic;
using System.Linq;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Interfaces.Systems;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Signals;
using JGajewski.Game.States.Gameplay.Interfaces;
using JGajewski.StateMachine.Interfaces;
using Zenject;

namespace JGajewski.Entities.Projectile.Systems
{
    public class ProjectilesSystemsManager : EntitySystemsManager<ProjectilesSystemsManager, IReadOnlyList<ProjectileEntity>>, 
        IGameplayStateAction, IStatePostEnterAction, IStatePreExitAction, IStatePostExitAction
    {
        public int Priority => 0;
        
        private readonly List<ProjectileEntity> _projectileDataItems;
        
        [Inject]
        private ProjectilesSystemsManager(SignalBus signalBus, List<IEntitySystem<ProjectilesSystemsManager, IReadOnlyList<ProjectileEntity>>> entitySystems) 
            : base(signalBus, entitySystems)
        {
            _projectileDataItems = new List<ProjectileEntity>();
        }
        
        public void PostEnterAction(IState currentState)
        {
            StartSystems(_projectileDataItems);
        }

        public void PreExitAction(IState currentState)
        {
            StopSystems();
        }
        
        public void PostExitAction(IState currentState)
        {
            ClearAllProjectiles();
        }

        public void CreateProjectile(Guid ownerGuid, ProjectileSpawnData projectileSpawnData)
        {
            var projectile = new ProjectileEntity(projectileSpawnData);
            
            SignalBus.Fire(new ProjectileCreatedSignal(projectile.EntityGuid, projectileSpawnData, ownerGuid));
            _projectileDataItems.Add(projectile);
        }

        public void DestroyProjectile(Guid projectileGuid)
        {
            var projectile = _projectileDataItems.Find(x => x.EntityGuid == projectileGuid);
            if (projectile == null) return;
            
            SignalBus.Fire(new ProjectileDestroyedSignal(projectileGuid));
            _projectileDataItems.Remove(projectile);
        }

        private void ClearAllProjectiles()
        {
            var projectilesCount = _projectileDataItems.Count;

            var projectilesToRemove = _projectileDataItems.ToList();
            for (int i = 0; i < projectilesCount; i++)
            {
                var projectile = projectilesToRemove[i];
                DestroyProjectile(projectile.EntityGuid);
            }
            _projectileDataItems.Clear();
        }
    }
}
