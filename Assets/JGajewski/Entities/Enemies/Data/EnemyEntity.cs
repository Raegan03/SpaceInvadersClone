using System;
using JGajewski.Entities.Interfaces;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Data
{
    public class EnemyEntity : IEntity
    {
        public Guid EntityGuid { get; }
        
        public EnemyType EnemyType { get; }
        
        public int EnemyScore { get; }
        
        public float EnemyProjectileSpeed { get; }

        public int ColumnIndex => EnemyColumnPositionIndexes.x;
        public int ColumnVerticalIndex => EnemyColumnPositionIndexes.y;
        public Vector2Int EnemyColumnPositionIndexes { get; }
        
        public EnemyEntity(EnemyTypeSettings enemyTypeSettings, Vector2Int enemyColumnPositionIndexes)
        {
            EntityGuid = Guid.NewGuid();
            
            EnemyType = enemyTypeSettings.EnemyType;
            EnemyScore = enemyTypeSettings.EnemyScore;
            EnemyProjectileSpeed = enemyTypeSettings.EnemyProjectileSpeed;

            EnemyColumnPositionIndexes = enemyColumnPositionIndexes;
        }
    }
}
