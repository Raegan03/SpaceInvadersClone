using JGajewski.Entities.Abstracts.Factories;
using JGajewski.Entities.Enemies.Views;
using JGajewski.Entities.Interfaces.Factories;
using Zenject;

namespace JGajewski.Entities.Enemies.Factories
{
    public class EnemiesWavePlaceholderFactory : EntityPlaceholderFactory<EnemiesWaveView>
    {
    }
    
    public class EnemiesWaveFactory : EntityFactory<EnemiesWaveView>
    {
        public EnemiesWaveFactory(DiContainer container, IEntityPrefabProvider<EnemiesWaveView> entityPrefabProvider) 
            : base(container, entityPrefabProvider)
        {
        }
    }
}
