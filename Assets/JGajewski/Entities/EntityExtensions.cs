using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Interfaces.Factories;
using JGajewski.Entities.Interfaces.Systems;
using Zenject;

namespace JGajewski.Entities
{
    public static class EntityExtensions
    {
        public static void BindEntityFactory<TView, TPlaceholderFactory, TFactory>(this DiContainer container)
            where TView : EntityView
            where TPlaceholderFactory : EntityPlaceholderFactory<TView>
            where TFactory : IEntityFactory<TView>
        {
            container.BindFactory<TView, TPlaceholderFactory>()
                .FromFactory<TFactory>();
        }
        
        public static void BindEntityMonoPool<TView, TMonoPool, TFactory>(this DiContainer container)
            where TView : EntityView
            where TMonoPool : EntityMonoPool<TView>
            where TFactory : IEntityFactory<TView>
        {
            container.BindMemoryPool<TView, TMonoPool>()
                .FromFactory<TFactory>();
        }

        public static void BindEntityMonoPool<TView, T1, TMonoPool, TFactory>(this DiContainer container)
            where TView : EntityView
            where TMonoPool : EntityMonoPool<T1, TView>
            where TFactory : IEntityFactory<TView>
        {
            container.BindMemoryPool<TView, TMonoPool>()
                .FromFactory<TFactory>();
        }
        
        public static void BindEntityMonoPool<TView, T1, T2, TMonoPool, TFactory>(this DiContainer container)
            where TView : EntityView
            where TMonoPool : EntityMonoPool<T1, T2, TView>
            where TFactory : IEntityFactory<TView>
        {
            container.BindMemoryPool<TView, TMonoPool>()
                .FromFactory<TFactory>();
        }
        
        public static void BindEntitySystem<TSystemManager, TSystem, TData>(this DiContainer container)
            where TSystemManager : IEntitySystemsManager<TSystemManager, TData>
            where TSystem : IEntitySystem<TSystemManager, TData>
        {
            container.Bind<IEntitySystem<TSystemManager, TData>>()
                .To<TSystem>()
                .WhenInjectedInto<TSystemManager>();
        }
    }
}
