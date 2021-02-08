using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Player.Signals;
using JGajewski.Entities.Projectile.Data;

namespace JGajewski.Entities.Player.Views
{
    public class PlayerWeapon : EntityWeapon<PlayerShootSignal>
    {
        public override ProjectileOwnerType ProjectileOwnerType => ProjectileOwnerType.Player;
    }
}
