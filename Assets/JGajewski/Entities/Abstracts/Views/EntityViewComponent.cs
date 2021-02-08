using UnityEngine;

namespace JGajewski.Entities.Abstracts.Views
{
    public abstract class EntityViewComponent : MonoBehaviour
    {
        protected EntityView EntityView { get; private set; }
        
        public void Populate(EntityView entityView)
        {
            EntityView = entityView;
            OnPopulated();
        }

        public void Clear()
        {
            OnCleared();
            EntityView = null;
        }

        protected abstract void OnPopulated();
        protected abstract void OnCleared();
    }
}
