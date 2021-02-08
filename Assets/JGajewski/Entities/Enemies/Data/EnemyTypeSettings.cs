using UnityEngine;

namespace JGajewski.Entities.Enemies.Data
{
    [CreateAssetMenu(menuName = "JGajewski/Enemies/EnemyTypeSettings", fileName = "EnemyTypeSettings")]
    public class EnemyTypeSettings : ScriptableObject
    {
        public EnemyType EnemyType => enemyType;
        
        public float EnemyProjectileSpeed => enemyProjectileSpeed;
        public int EnemyScore => enemyScore;
        
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float enemyProjectileSpeed;
        [SerializeField] private int enemyScore;
    }
}
