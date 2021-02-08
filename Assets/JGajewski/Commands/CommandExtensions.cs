using Zenject;

namespace JGajewski.Commands
{
    public static class CommandExtensions
    {
        public static void BindCommandBus(this DiContainer container)
        {
            container.Bind<CommandBus>()
                .AsSingle();
        }
        
        public static void BindCommandHandler<T, TH>(this DiContainer container)
            where T : ICommand
            where TH : ICommandHandler
        {
            container.Bind<ICommandHandler>()
                .WithId(typeof(T))
                .To<TH>()
                .AsSingle();
        }
    }
}
