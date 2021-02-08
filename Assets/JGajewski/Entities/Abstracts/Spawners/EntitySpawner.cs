using System;
using System.Collections.Generic;
using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Interfaces.Signals;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Abstracts.Spawners
{
    public abstract class EntitySpawner<TFactory, TView, TCreatedSignal, TDestroyedSignal> : MonoBehaviour, IDisposable
        where TView : EntityView
        where TFactory : EntityPlaceholderFactory<TView>
        where TCreatedSignal : IEntityCreatedSignal
        where TDestroyedSignal : IEntityDestroyedSignal
    {
        [SerializeField] private Transform entitiesRoot;
        
        protected Dictionary<Guid, EntityView> SpawnedViews;
        
        private SignalBus _signalBus;
        private TFactory _entityFactory;

        [Inject]
        private void AbstractConstruct(SignalBus signalBus, TFactory entityFactory)
        {
            _signalBus = signalBus;
            _entityFactory = entityFactory;

            SpawnedViews = new Dictionary<Guid, EntityView>();
            
            _signalBus.Subscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Subscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Unsubscribe<TDestroyedSignal>(HandleEntityDestroyed);
            Dispose();
        }

        public virtual void Dispose()
        {
        }

        protected virtual void HandleEntityCreated(TCreatedSignal s)
        {
            var entity = _entityFactory.Create();
            entity.Populate(s.EntityGuid, entitiesRoot);
            
            SpawnedViews.Add(s.EntityGuid, entity);
        }
        
        protected virtual void HandleEntityDestroyed(TDestroyedSignal s)
        {
            if (!SpawnedViews.TryGetValue(s.EntityGuid, out var entityView))
            {
                Debug.LogError($"Couldn't find entity to destroy [{s.EntityGuid}]");
                return;
            }
            
            entityView.Clear();
            
            Destroy(entityView.gameObject);
            SpawnedViews.Remove(s.EntityGuid);
        }
    }
    
    public abstract class EntitySpawner<TFactory, T1, TView, TCreatedSignal, TDestroyedSignal> : MonoBehaviour, IDisposable
        where TView : EntityView
        where TFactory : EntityPlaceholderFactory<TView>
        where TCreatedSignal : IEntityCreatedSignal<T1>
        where TDestroyedSignal : IEntityDestroyedSignal
    {
        [SerializeField] private Transform entitiesRoot;
        
        protected Dictionary<Guid, EntityView> SpawnedViews;
        
        private SignalBus _signalBus;
        private TFactory _entityFactory;

        [Inject]
        private void AbstractConstruct(SignalBus signalBus, TFactory entityFactory)
        {
            _signalBus = signalBus;
            _entityFactory = entityFactory;

            SpawnedViews = new Dictionary<Guid, EntityView>();
            
            _signalBus.Subscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Subscribe<TDestroyedSignal>(HandleEntityDestroyed);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<TCreatedSignal>(HandleEntityCreated);
            _signalBus.Unsubscribe<TDestroyedSignal>(HandleEntityDestroyed);
            Dispose();
        }

        public virtual void Dispose()
        {
        }

        protected virtual void HandleEntityCreated(TCreatedSignal s)
        {
            var entity = _entityFactory.Create();
            entity.Populate(s.EntityGuid, entitiesRoot);
            
            SpawnedViews.Add(s.EntityGuid, entity);
        }
        
        protected virtual void HandleEntityDestroyed(TDestroyedSignal s)
        {
            if (!SpawnedViews.TryGetValue(s.EntityGuid, out var entityView))
            {
                Debug.LogError($"Couldn't find entity to destroy [{s.EntityGuid}]");
                return;
            }
            
            entityView.Clear();
            
            Destroy(entityView.gameObject);
            SpawnedViews.Remove(s.EntityGuid);
        }
    }
}
