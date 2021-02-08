using JGajewski.Entities.Abstracts.Views;
using Zenject;

namespace JGajewski.Entities.Interfaces.Factories
{
    public interface IEntityFactory<out TObject> : IFactory<TObject>
        where TObject : EntityView
    {
    }
}