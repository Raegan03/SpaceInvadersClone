using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Entities.Projectile.Data;

namespace JGajewski.Entities.Enemies.Views
{
    public class EnemyWeapon : EntityWeapon<EnemyShootSignal>
    {
        public override ProjectileOwnerType ProjectileOwnerType => ProjectileOwnerType.Enemy;
    }
}
