using System.Collections.Generic;
using JGajewski.Common;
using JGajewski.Entities.Enemies.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace JGajewski.Entities.Enemies.Data
{
    [CreateAssetMenu(menuName = "JGajewski/Enemies/EnemiesSettings", fileName = "EnemiesSettings")]
    public class EnemiesSettings : ScriptableObject
    {
        public IReadOnlyList<EnemyTypeSettings> EnemySubTypeSettingsList => enemySubTypeSettings;
        public AssetReference EnemyPrefabReference => enemyPrefabReference;
        public EnemiesWaveView EnemiesWaveView => enemiesWaveView;

        [Header("Enemies Sub Types")]
        [SerializeField] private EnemyTypeSettings[] enemySubTypeSettings;
        [SerializeField] private AssetReference enemyPrefabReference;

        [SerializeField] private EnemiesWaveView enemiesWaveView;
        
        public int LowestWaveVerticalIndex => lowestWaveVerticalIndex;
        public int EnemiesInRow => enemiesInRow;
        public int RowsInWave => rowsInWave;
        public Range WaveHorizontalOffset => waveHorizontalOffset;
        
        [Header("Enemies Wave Settings")]
        [SerializeField] private int lowestWaveVerticalIndex;
        [SerializeField] private int enemiesInRow;
        [SerializeField] private int rowsInWave;
        [SerializeField] private Range waveHorizontalOffset;
        
        public float HorizontalMoveSpeed => horizontalMoveSpeed;
        public float VerticalMoveSpeed => verticalMoveSpeed;
        
        [Header("Enemies Move Settings")]
        [SerializeField] private float horizontalMoveSpeed;
        [SerializeField] private float verticalMoveSpeed;
        
        public float ShootChance => shootChance;
        public float ShootDelay => shootDelay;
        public int MaxProjectilesCountAtOnce => maxProjectilesCountAtOnce;
        
        [Header("Enemies Shooting Settings")]
        [SerializeField] private float shootChance;
        [SerializeField] private float shootDelay;
        [SerializeField] private int maxProjectilesCountAtOnce;
    }
}
