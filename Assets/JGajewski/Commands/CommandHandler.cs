using UnityEngine.Events;

namespace JGajewski.Commands
{
    public abstract class CommandHandler<T> : ICommandHandler
        where T : struct, ICommand
    {
        UnityAction<ICommand> ICommandHandler.Execute => 
            command => ExplicitExecute((T) command);

        protected abstract void ExplicitExecute(T command);
    }
}
