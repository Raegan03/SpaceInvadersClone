using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Enemies.Data;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Views
{
    public class EnemyView : EntityView
    {
        [SerializeField] private EnemyTypeView[] enemyTypeViews;

        private EnemyTypeView _currentEnemyTypeView;

        private EntityViewComponent[] _cachedComponents;
        
        private void Awake()
        {
            _cachedComponents = entityViewComponents;
        }

        public void Setup(Vector3 position, EnemyType enemyType)
        {
            transform.position = position;

            var enemyTypeViewsLength = enemyTypeViews.Length;
            for (int i = 0; i < enemyTypeViewsLength; i++)
            {
                var enemyTypeView = enemyTypeViews[i];
                if (enemyTypeView.EnemyType != enemyType)
                {
                    enemyTypeView.Disable();
                    continue;
                }

                _currentEnemyTypeView = enemyTypeView;
                _currentEnemyTypeView.Enable();

                var typeViewComponents = _currentEnemyTypeView.EntityViewComponents;

                var entityViewComponentsLength = entityViewComponents.Length;
                var typeViewComponentsLength = typeViewComponents.Length;
                
                var viewComponents = new EntityViewComponent[entityViewComponentsLength + typeViewComponentsLength];
                var index = 0;
                
                for (int k = 0; k < entityViewComponentsLength; k++)
                {
                    viewComponents[index] = entityViewComponents[k];
                    index++;
                }
                
                for (int k = 0; k < typeViewComponentsLength; k++)
                {
                    viewComponents[index] = typeViewComponents[k];
                    index++;
                }

                entityViewComponents = viewComponents;
            }
        }

        public override void Clear()
        {
            base.Clear();
            entityViewComponents = _cachedComponents;
        }
    }
}
