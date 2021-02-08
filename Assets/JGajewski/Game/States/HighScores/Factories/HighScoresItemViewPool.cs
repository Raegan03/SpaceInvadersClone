using System;
using JGajewski.Game.States.HighScores.Views;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.States.HighScores.Factories
{
    public class HighScoresItemViewPool : MonoMemoryPool<Transform, int, DateTime, int, HighScoreItemView>
    {
        protected override void Reinitialize(Transform parent, int number, DateTime dateTime, int score, HighScoreItemView item)
        {
            base.Reinitialize(parent, number, dateTime, score, item);
            
            item.transform.SetParent(parent, false);
            item.Populate(number, dateTime, score);
        }

        protected override void OnDespawned(HighScoreItemView item)
        {
            base.OnDespawned(item);
            item.Clear();
        }
    }
}
