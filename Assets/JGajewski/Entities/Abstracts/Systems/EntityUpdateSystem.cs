﻿using JGajewski.Entities.Interfaces.Systems;

namespace JGajewski.Entities.Abstracts.Systems
{
    public abstract class EntityUpdateSystem<TController, TData> : IEntityUpdateSystem<TController, TData>
        where TController : IEntitySystemsManager<TController, TData>
    {
        public TController Manager { get; protected set; }
        public TData Data { get; protected set; }

        protected bool IsRunning => Started && !Paused;
        
        protected bool Started { get; set; }
        protected bool Paused { get; set; }

        public virtual void Start(TController manager, TData data)
        {
            Manager = manager;
            Data = data;
            
            Started = true;
        }

        public virtual void Stop()
        {
            Started = false;
        }

        public virtual void Pause()
        {
            Paused = true;
        }

        public virtual void Resume()
        {
            Paused = false;
        }

        public virtual void Update()
        {
            if(!IsRunning) return;
        }
    }
}
