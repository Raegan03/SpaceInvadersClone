using JGajewski.Entities.Abstracts.Views;

namespace JGajewski.Entities.Interfaces.Factories
{
    public interface IEntityPrefabProvider<out TObject> 
        where TObject : EntityView
    {
        TObject GetPrefab();
    }
}