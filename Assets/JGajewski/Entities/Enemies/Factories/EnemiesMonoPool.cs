using System;
using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Views;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Factories
{
    public class EnemiesMonoPool : EntityMonoPool<Vector3, EnemyType, EnemyView>
    {
        protected override void Reinitialize(Vector3 p1, EnemyType p2, Transform entityParent, Guid entityGuid, EnemyView item)
        {
            item.Setup(p1, p2);
            base.Reinitialize(p1, p2, entityParent, entityGuid, item);
        }
    }
}
