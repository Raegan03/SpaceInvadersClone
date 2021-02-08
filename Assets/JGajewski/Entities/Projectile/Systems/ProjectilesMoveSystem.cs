using System.Collections.Generic;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Signals;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Systems
{
    public class ProjectilesMoveSystem : EntityUpdateSystem<ProjectilesSystemsManager, IReadOnlyList<ProjectileEntity>>
    {
        public override void Update()
        {
            base.Update();
            
            var projectilesCount = Data.Count;
            if(projectilesCount == 0) return;
            
            var projectilesMovedSignal = new ProjectilesMovedSignal();
            
            for (int i = 0; i < projectilesCount; i++)
            {
                var projectileData = Data[i];
                projectileData.UpdatePosition(Time.deltaTime);
                
                projectilesMovedSignal.AddProjectilePosition(
                    projectileData.EntityGuid, projectileData.ProjectilePosition);
            }
            
            Manager.SignalBus.Fire(projectilesMovedSignal);
        }
    }
}
