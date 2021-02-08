using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Interfaces.Factories;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Abstracts.Factories
{
    public abstract class EntityFactory<TObject> : IEntityFactory<TObject>
        where TObject : EntityView
    {
        private readonly DiContainer _container;
        private readonly IEntityPrefabProvider<TObject> _entityPrefabProvider;
        
        [Inject]
        protected EntityFactory(DiContainer container, 
            IEntityPrefabProvider<TObject> entityPrefabProvider)
        {
            _container = container;
            _entityPrefabProvider = entityPrefabProvider;
        }

        public TObject Create()
        {
            var instantiatedView = _container
                .InstantiatePrefabForComponent<TObject>(_entityPrefabProvider.GetPrefab());

            if (instantiatedView.TryGetComponent<TObject>(out var entityView)) return entityView;
            
            Debug.LogError($"Can't get EntityView from prefab {instantiatedView.name}!");
            return null;
        }
    }
}