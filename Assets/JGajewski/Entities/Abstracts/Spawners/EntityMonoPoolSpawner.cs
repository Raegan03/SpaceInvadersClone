using System;
using System.Collections.Generic;
using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Abstracts.Spawners
{
    public abstract class EntityMonoPoolSpawner<TMonoPool, TView, TCreatedSignal, TDestroyedSignal> : MonoBehaviour
        where TView : EntityView
        where TMonoPool : EntityMonoPool<TView>
        where TCreatedSignal : IEntityCreatedSignal
        where TDestroyedSignal : IEntityDestroyedSignal
    {
        [SerializeField] private Transform poolRoot;
        
        protected Dictionary<Guid, TView> SpawnedViews;
        
        private SignalBus _signalBus;
        private TMonoPool _entityMonoPool;

        [Inject]
        private void AbstractConstruct(SignalBus signalBus, TMonoPool entityMonoPool)
        {
            _signalBus = signalBus;
            _entityMonoPool = entityMonoPool;

            SpawnedViews = new Dictionary<Guid, TView>();
        }
        
        private void OnDestroy()
        {
            _entityMonoPool.Clear();
        }

        protected void OnEnable()
        {
            _signalBus.Subscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Subscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Unsubscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }

        protected virtual void HandleEntityCreated(TCreatedSignal s)
        {
            var entity = _entityMonoPool.Spawn(poolRoot, s.EntityGuid);
            SpawnedViews.Add(s.EntityGuid, entity);
        }
        
        protected virtual void HandleEntityDestroyed(TDestroyedSignal s)
        {
            if (!SpawnedViews.TryGetValue(s.EntityGuid, out var entityView))
            {
                Debug.LogError($"Couldn't find entity to destroy [{s.EntityGuid}]");
                return;
            }
            
            _entityMonoPool.Despawn(entityView);
            SpawnedViews.Remove(s.EntityGuid);
        }
    }
    
    public abstract class EntityMonoPoolSpawner<T1, TMonoPool, TView, TCreatedSignal, TDestroyedSignal> : MonoBehaviour
        where TView : EntityView
        where TMonoPool : EntityMonoPool<T1, TView>
        where TCreatedSignal : IEntityCreatedSignal<T1>
        where TDestroyedSignal : IEntityDestroyedSignal
    {
        [SerializeField] private Transform poolRoot;
        
        protected Dictionary<Guid, TView> SpawnedViews;
        
        private SignalBus _signalBus;
        private TMonoPool _entityMonoPool;

        [Inject]
        private void AbstractConstruct(SignalBus signalBus, TMonoPool entityMonoPool)
        {
            _signalBus = signalBus;
            _entityMonoPool = entityMonoPool;

            SpawnedViews = new Dictionary<Guid, TView>();
        }
        
        private void OnDestroy()
        {
            _entityMonoPool.Clear();
        }

        protected void OnEnable()
        {
            _signalBus.Subscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Subscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Unsubscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }
        
        protected virtual void HandleEntityCreated(TCreatedSignal s)
        {
            var entity = _entityMonoPool.Spawn(s.P1, poolRoot, s.EntityGuid);
            SpawnedViews.Add(s.EntityGuid, entity);
        }
        
        protected virtual void HandleEntityDestroyed(TDestroyedSignal s)
        {
            if (!SpawnedViews.TryGetValue(s.EntityGuid, out var entityView))
            {
                Debug.LogError($"Couldn't find entity to destroy [{s.EntityGuid}]");
                return;
            }
            
            _entityMonoPool.Despawn(entityView);
            SpawnedViews.Remove(s.EntityGuid);
        }
    }
    
    public abstract class EntityMonoPoolSpawner<T1, T2, TMonoPool, TView, TCreatedSignal, TDestroyedSignal> : MonoBehaviour
        where TView : EntityView
        where TMonoPool : EntityMonoPool<T1, T2, TView>
        where TCreatedSignal : IEntityCreatedSignal<T1, T2>
        where TDestroyedSignal : IEntityDestroyedSignal
    {
        [SerializeField] private Transform poolRoot;
        
        protected Dictionary<Guid, TView> SpawnedViews;
        
        private SignalBus _signalBus;
        private TMonoPool _entityMonoPool;

        [Inject]
        private void AbstractConstruct(SignalBus signalBus, TMonoPool entityMonoPool)
        {
            _signalBus = signalBus;
            _entityMonoPool = entityMonoPool;

            SpawnedViews = new Dictionary<Guid, TView>();
        }

        private void OnDestroy()
        {
            _entityMonoPool.Clear();
        }

        protected void OnEnable()
        {
            _signalBus.Subscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Subscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Unsubscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }
        
        protected virtual void HandleEntityCreated(TCreatedSignal s)
        {
            var entity = _entityMonoPool.Spawn(s.P1, s.P2, poolRoot, s.EntityGuid);
            SpawnedViews.Add(s.EntityGuid, entity);
        }
        
        protected virtual void HandleEntityDestroyed(TDestroyedSignal s)
        {
            if (!SpawnedViews.TryGetValue(s.EntityGuid, out var entityView))
            {
                Debug.LogError($"Couldn't find entity to destroy [{s.EntityGuid}]");
                return;
            }
            
            _entityMonoPool.Despawn(entityView);
            SpawnedViews.Remove(s.EntityGuid);
        }
    }
}
