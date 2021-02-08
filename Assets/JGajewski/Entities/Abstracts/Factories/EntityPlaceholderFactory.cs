using JGajewski.Entities.Abstracts.Views;
using Zenject;

namespace JGajewski.Entities.Abstracts.Factories
{
    public abstract class EntityPlaceholderFactory<TObject> : PlaceholderFactory<TObject> 
        where TObject : EntityView
    {
    }
}