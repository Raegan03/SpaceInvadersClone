using System;
using JGajewski.Commands;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities.Abstracts.Views
{
    public abstract class EntityView : MonoBehaviour
    {
        [SerializeField] protected EntityViewComponent[] entityViewComponents;

        public Guid EntityGuid { get; protected set; } = Guid.Empty;
        
        public SignalBus SignalBus => _signalBus;
        public CommandBus CommandBus => _commandBus;
        
        private SignalBus _signalBus;
        private CommandBus _commandBus;

        [Inject]
        private void EntityConstruct(SignalBus signalBus, CommandBus commandBus)
        {
            _signalBus = signalBus;
            _commandBus = commandBus;
        }

        public virtual void Populate(Guid entityGuid, Transform entityParent)
        {
            EntityGuid = entityGuid;
            transform.SetParent(entityParent);

            PopulateComponents();
        }

        public virtual void Clear()
        {
            ClearComponents();
            EntityGuid = Guid.Empty;
        }

        protected virtual void PopulateComponents()
        {
            var componentsLength = entityViewComponents.Length;
            for (int i = 0; i < componentsLength; i++)
            {
                entityViewComponents[i].Populate(this);
            }
        }
        
        protected virtual void ClearComponents()
        {
            var componentsLength = entityViewComponents.Length;
            for (int i = 0; i < componentsLength; i++)
            {
                entityViewComponents[i].Clear();
            }
        }
    }
}
