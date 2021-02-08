using System;
using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Projectile.Data;
using JGajewski.Entities.Projectile.Views;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Factories
{
    public class ProjectilesMonoPool : EntityMonoPool<ProjectileSpawnData, ProjectileView>
    {
        protected override void Reinitialize(ProjectileSpawnData p1, 
            Transform entityParent, Guid entityGuid, ProjectileView item)
        {
            item.Setup(p1);
            base.Reinitialize(p1, entityParent, entityGuid, item);
        }
    }
}
