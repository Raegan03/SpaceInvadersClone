using System;
using System.Collections.Generic;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Signals
{
    public class ProjectilesMovedSignal
    {
        public readonly Dictionary<Guid, Vector3> ProjectilesPositions;
        
        public ProjectilesMovedSignal()
        {
            ProjectilesPositions = new Dictionary<Guid, Vector3>();
        }

        public void AddProjectilePosition(Guid projectileGuid, Vector3 projectilePosition)
        {
            ProjectilesPositions.Add(projectileGuid, projectilePosition);
        }
    }
}
