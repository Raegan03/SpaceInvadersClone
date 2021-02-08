using System;
using TMPro;
using UnityEngine;

namespace JGajewski.Game.States.HighScores.Views
{
    public class HighScoreItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberLabel;
        [SerializeField] private TextMeshProUGUI dateLabel;
        [SerializeField] private TextMeshProUGUI scoreLabel;

        public void Populate(int number, DateTime dateTime, int score)
        {
            numberLabel.text = $"{number}.";
            dateLabel.text = $"{dateTime}";
            scoreLabel.text = $"{score}";
        }

        public void Clear()
        {
            numberLabel.text = string.Empty;
            dateLabel.text = string.Empty;
            scoreLabel.text = string.Empty;
        }
    }
}
