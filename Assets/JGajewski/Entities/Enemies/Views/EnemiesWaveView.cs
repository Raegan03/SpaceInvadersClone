using JGajewski.Entities.Abstracts.Views;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Views
{
    public class EnemiesWaveView : EntityView
    {
        public void SetupPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
