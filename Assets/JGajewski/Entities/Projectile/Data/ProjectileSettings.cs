using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JGajewski.Entities.Projectile.Data
{
    [CreateAssetMenu(menuName = "JGajewski/Projectile/Settings", fileName = "ProjectileSettings", order = 0)]
    public class ProjectileSettings : ScriptableObject
    {
        public AssetReference ProjectilePrefabReference => projectilePrefabReference;
        
        [SerializeField] private AssetReference projectilePrefabReference;
    }
}
