using System.Collections.Generic;
using JGajewski.Entities.Interfaces.Systems;
using Zenject;

namespace JGajewski.Entities.Abstracts.Systems
{
    public abstract class EntitySystemsManager<TController, TData> : IEntitySystemsManager<TController, TData>, ITickable
        where TController : EntitySystemsManager<TController, TData>
    {
        public bool IsEnabled { get; protected set; }
        
        public List<IEntitySystem<TController, TData>> Systems { get; protected set; }
        public List<IEntityUpdateSystem<TController, TData>> UpdateSystems { get; protected set; }

        public SignalBus SignalBus => _signalBus;
        private readonly SignalBus _signalBus; 

        protected EntitySystemsManager(SignalBus signalBus, List<IEntitySystem<TController, TData>> subControllers)
        {
            _signalBus = signalBus;
            
            Systems = subControllers;
            UpdateSystems = new List<IEntityUpdateSystem<TController, TData>>();

            var systemsCount = Systems.Count;
            for (int i = 0; i < systemsCount; i++)
            {
                var system = Systems[i];
                if (system is IEntityUpdateSystem<TController, TData> updateSystem)
                {
                    UpdateSystems.Add(updateSystem);
                }
            }
        }

        protected void StartSystems(TData data)
        {
            var systemsCount = Systems.Count;
            for (int i = 0; i < systemsCount; i++)
            {
                Systems[i].Start(this as TController, data);
            }

            IsEnabled = true;
        }

        protected void StopSystems()
        {
            var systemsCount = Systems.Count;
            for (int i = 0; i < systemsCount; i++)
            {
                Systems[i].Stop();
            }
            
            IsEnabled = false;
        }
        
        protected void PauseSystems()
        {
            var systemsCount = Systems.Count;
            for (int i = 0; i < systemsCount; i++)
            {
                Systems[i].Pause();
            }
        }
        
        protected void ResumeSystems()
        {
            var systemsCount = Systems.Count;
            for (int i = 0; i < systemsCount; i++)
            {
                Systems[i].Resume();
            }
        }

        public virtual void Tick()
        {
            if (!IsEnabled) return;
            
            var updateSystemsCount = UpdateSystems.Count;
            for (int i = 0; i < updateSystemsCount; i++)
            {
                UpdateSystems[i].Update();
            }
        }
    }
}
