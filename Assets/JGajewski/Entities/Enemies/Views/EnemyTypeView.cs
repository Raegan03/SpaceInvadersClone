using JGajewski.Entities.Abstracts.Views;
using JGajewski.Entities.Enemies.Data;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Views
{
    public class EnemyTypeView : MonoBehaviour
    {
        public EnemyType EnemyType => enemyType;
        public EntityViewComponent[] EntityViewComponents => entityViewComponents;
        
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EntityViewComponent[] entityViewComponents;

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
