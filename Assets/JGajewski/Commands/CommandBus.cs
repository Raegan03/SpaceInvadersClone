using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace JGajewski.Commands
{
    public class CommandBus
    {
        private readonly DiContainer _container;
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;

        [Inject]
        public CommandBus(DiContainer container)
        {
            _container = container;
            _commandHandlers = new Dictionary<Type, ICommandHandler>();
        }
        
        public void Send(ICommand command)
        {
            if (command == null)
            {
                Debug.LogError("Command is null");
                return;
            }

            if (!_commandHandlers.TryGetValue(command.GetType(), out var commandHandler))
            {
                commandHandler = _container.TryResolveId<ICommandHandler>(command.GetType());
                if (commandHandler != null) _commandHandlers.Add(command.GetType(), commandHandler);
            }

            if (commandHandler == null)
            {
                Debug.LogError($"Command handler for command {command.GetType()} is null");
                return;
            }
            
            commandHandler.Execute(command);
        }
    }
}