#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace JGajewski.Game.Data.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameSettings))]
    public class GameSettingsEditor : UnityEditor.Editor
    {
        private GameSettings _gameSettings;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!_gameSettings)
            {
                _gameSettings = target as GameSettings;
                if(!_gameSettings) return;
            }

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Generate vertical positions"))
            {
                _gameSettings.GenerateVerticalPositions();
                EditorUtility.SetDirty(_gameSettings);
            }
        }
    }
#endif
}
