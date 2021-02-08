using System;
using JGajewski.Entities.Abstracts.Views;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Abstracts.Factories
{
    public abstract class EntityMonoPool<TView> : MonoMemoryPool<Transform, Guid, TView>
        where TView : EntityView
    {
        protected override void Reinitialize(Transform entityParent, Guid entityGuid, TView item)
        {
            base.Reinitialize(entityParent, entityGuid, item);
            item.Populate(entityGuid, entityParent);
        }

        protected override void OnDespawned(TView item)
        {
            item.Clear();
            base.OnDespawned(item);
        }
    }
    
    public abstract class EntityMonoPool<T1, TView> : MonoMemoryPool<T1, Transform, Guid, TView>
        where TView : EntityView
    {
        protected override void Reinitialize(T1 p1, Transform entityParent, Guid entityGuid, TView item)
        {
            base.Reinitialize(p1, entityParent, entityGuid, item);
            item.Populate(entityGuid, entityParent);
        }

        protected override void OnDespawned(TView item)
        {
            item.Clear();
            base.OnDespawned(item);
        }
    }
    
    public abstract class EntityMonoPool<T1, T2, TView> : MonoMemoryPool<T1, T2, Transform, Guid, TView>
        where TView : EntityView
    {
        protected override void Reinitialize(T1 p1, T2 p2, Transform entityParent, Guid entityGuid, TView item)
        {
            base.Reinitialize(p1, p2, entityParent, entityGuid, item);
            item.Populate(entityGuid, entityParent);
        }

        protected override void OnDespawned(TView item)
        {
            item.Clear();
            base.OnDespawned(item);
        }
    }
}
