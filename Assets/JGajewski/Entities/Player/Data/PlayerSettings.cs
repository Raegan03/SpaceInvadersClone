using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JGajewski.Entities.Player.Data
{
    [CreateAssetMenu(menuName = "JGajewski/Player/Settings", fileName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public AssetReference PlayerPrefabReference => playerPrefabReference;

        [Header("Player Prefab Settings")]
        [SerializeField] private AssetReference playerPrefabReference;
        
        public int PlayerStartLives => playerStartLives;
        public float PlayerProjectileSpeed => playerProjectileSpeed;
        public float PlayerSpeed => playerSpeed;
        public float PlayerProtectionDuration => playerProtectionDuration;
        
        [Header("Player Stats")]
        [SerializeField] private int playerStartLives;
        [SerializeField] private float playerProjectileSpeed;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float playerProtectionDuration;
    }
}
