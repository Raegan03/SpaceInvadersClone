using Cysharp.Threading.Tasks;
using JGajewski.Commands;
using JGajewski.Game.Commands;
using UnityEngine;
using Zenject;

namespace JGajewski.Game
{
    public class Bootstrap : MonoBehaviour
    {
        private CommandBus _commandBus;
        private LoadGameCommand _loadGameCommand;
    
        [Inject]
        private void Construct(CommandBus commandBus)
        {
            _commandBus = commandBus;
            _loadGameCommand = new LoadGameCommand();
        }

        private async UniTaskVoid Start()
        {
            await UniTask.Yield();
            _commandBus.Send(_loadGameCommand);
        }
    }
}
