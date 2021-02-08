using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Interfaces.Factories;
using JGajewski.Entities.Player.Views;
using Zenject;

namespace JGajewski.Entities.Player.Factories
{
    public class PlayerFactory : EntityPlaceholderFactory<PlayerView> {}
    public class PlayerEntityFactory : EntityFactory<PlayerView>
    {
        [Inject]
        public PlayerEntityFactory(DiContainer container, 
            IEntityPrefabProvider<PlayerView> entityPrefabProvider) 
            : base(container, entityPrefabProvider)
        {
        }
    }
}
