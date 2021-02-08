using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Projectile.Data;
using UnityEngine;

namespace JGajewski.Entities.Projectile.Views
{
    public class ProjectileView : EntityView
    {
        public void Setup(ProjectileSpawnData projectileSpawnData)
        {
            switch (projectileSpawnData.ProjectileOwnerType)
            {
                case ProjectileOwnerType.Player:
                    gameObject.layer = LayerMask.NameToLayer("Player");
                    break;
                case ProjectileOwnerType.Enemy:
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    break;
            }
            
            var projectileTransform = transform;
            projectileTransform.position = projectileSpawnData.SpawnPosition;
            projectileTransform.forward = -projectileSpawnData.Direction;
        }
    }
}
