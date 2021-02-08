using JGajewski.Entities.Abstracts.Views;
using UnityEngine;

namespace JGajewski.Entities.Player.Views
{
    public class PlayerView : EntityView
    {
        public void SetupPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
