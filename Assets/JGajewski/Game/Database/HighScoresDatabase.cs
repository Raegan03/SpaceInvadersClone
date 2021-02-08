using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using JGajewski.Game.Data;
using JGajewski.Game.Database.Data;
using JGajewski.Game.States.Loading.Interfaces;
using JGajewski.StateMachine.Interfaces;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.Database
{
    public class HighScoresDatabase : ILoadingStateAction
    {
        public int Priority => 0;

        private List<HighScoreEntry> _highScoreEntities;
        public IReadOnlyList<HighScoreEntry> HighScoreEntities => _highScoreEntities;
        
        private readonly GameSettings _gameSettings;

        [Inject]
        public HighScoresDatabase(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            Debug.Assert(!string.IsNullOrWhiteSpace(_gameSettings.HighScoresSaveFileName), 
                "High scores save file name not specified!");
        }

        public async UniTask EnterAction(IState currentState)
        {
            LoadOrCreateDatabase();
        }

        public void AddNewHighScore(int score, DateTime dateTime)
        {
            _highScoreEntities.Add(new HighScoreEntry(score, dateTime));
            _highScoreEntities = _highScoreEntities.OrderByDescending(x => x.Score).ToList();
            
            SaveDatabase();
        }

        private void LoadOrCreateDatabase()
        {
            var databasePath = GetDatabasePath(_gameSettings.HighScoresSaveFileName);
            if (!File.Exists(databasePath))
            {
                _highScoreEntities = new List<HighScoreEntry>();
                SaveDatabase();
            }
            else
            {
                var entitiesJson = File.ReadAllText(databasePath);
                _highScoreEntities = JsonConvert.DeserializeObject<List<HighScoreEntry>>(entitiesJson);
            }
        }

        private void SaveDatabase()
        {
            var databasePath = GetDatabasePath(_gameSettings.HighScoresSaveFileName);
            
            var entitiesJson = JsonConvert.SerializeObject(_highScoreEntities);
            File.WriteAllText(databasePath, entitiesJson);
        }
        
        private string GetDatabasePath(string fileName) => 
            Path.Combine(Application.persistentDataPath, fileName);
    }
}
