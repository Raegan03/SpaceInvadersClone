using UnityEngine.Events;

namespace JGajewski.Commands
{
    public interface ICommandHandler
    {
        UnityAction<ICommand> Execute { get; }
    }
}
