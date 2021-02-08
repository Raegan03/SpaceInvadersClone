using JGajewski.Entities.Abstracts.Spawners;
using JGajewski.Entities.Player.Factories;
using JGajewski.Entities.Player.Signals;
using UnityEngine;

namespace JGajewski.Entities.Player.Views
{
    public class PlayerSpawner : EntitySpawner<PlayerFactory, 
        Vector3, PlayerView, PlayerCreatedSignal, PlayerDestroyedSignal>
    {
        protected override void HandleEntityCreated(PlayerCreatedSignal s)
        {
            base.HandleEntityCreated(s);
            if(!SpawnedViews.TryGetValue(s.EntityGuid, out var view)) return;

            var playerView = (PlayerView)view;
            playerView.SetupPosition(s.P1);
        }
    }
}
