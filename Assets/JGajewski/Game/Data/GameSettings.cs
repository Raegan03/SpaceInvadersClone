using System.Collections.Generic;
using JGajewski.Common;
using UnityEngine;

namespace JGajewski.Game.Data
{
    [CreateAssetMenu(menuName = "JGajewski/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public Range HorizontalGameViewRange => horizontalGameViewRange;
        public Range VerticalGameViewRange => verticalGameViewRange;
        public List<float> VerticalPositions => verticalPositions;
        public float VerticalPositionStep => verticalPositionStep;

        [Header("Game View Settings")] 
        [SerializeField] private Range horizontalGameViewRange;
        [SerializeField] private Range verticalGameViewRange;
        [SerializeField] private List<float> verticalPositions;
        [SerializeField] private float verticalPositionStep;
        [SerializeField] private int verticalLines;
        
        public string HighScoresSaveFileName => highScoresSaveFileName;
        public int HighScoresCountOnList => highScoresCountOnList;

        [Header("High Scores Database Settings")] 
        [SerializeField] private string highScoresSaveFileName;
        [SerializeField] private int highScoresCountOnList;

        public float TransitionFadesDuration => transitionFadesDuration;
        
        [Header("UI Settings")] 
        [SerializeField] private float transitionFadesDuration;

        public void GenerateVerticalPositions()
        {
            if(verticalLines < 2) return;
            
            verticalPositions = verticalGameViewRange
                .GetPositionsInRange(verticalLines);
            verticalPositionStep = Mathf.Abs(VerticalPositions[0] - VerticalPositions[1]);
        }
    }
}
