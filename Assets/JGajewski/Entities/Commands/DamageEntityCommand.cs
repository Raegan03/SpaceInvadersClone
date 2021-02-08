using System;
using JGajewski.Commands;
using JGajewski.Entities.Enemies.Systems;
using JGajewski.Entities.Player.Systems;
using Zenject;

namespace JGajewski.Entities.Commands
{
    public struct DamageEntityCommand : ICommand
    {
        public readonly Guid EntityGuid;
        public readonly EntityType EntityType;

        public DamageEntityCommand(Guid entityGuid, EntityType entityType)
        {
            EntityGuid = entityGuid;
            EntityType = entityType;
        }
    }

    public class DamageEntityCommandHandler : CommandHandler<DamageEntityCommand>
    {
        private readonly PlayerSystemsManager _playerSystemsManager;
        private readonly EnemiesSystemsManager _enemiesSystemsManager;
        
        [Inject]
        public DamageEntityCommandHandler(PlayerSystemsManager playerSystemsManager, 
            EnemiesSystemsManager enemiesSystemsManager)
        {
            _playerSystemsManager = playerSystemsManager;
            _enemiesSystemsManager = enemiesSystemsManager;
        }
        
        protected override void ExplicitExecute(DamageEntityCommand command)
        {
            switch (command.EntityType)
            {
                case EntityType.Player:
                    _playerSystemsManager.DamagePlayer();
                    break;
                case EntityType.Enemy:
                    _enemiesSystemsManager.DestroyEnemyEntity(command.EntityGuid);
                    break;
            }
        }
    }
}