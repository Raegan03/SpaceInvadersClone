using System;
using System.Collections.Generic;
using JGajewski.Common;
using JGajewski.Entities.Interfaces;
using JGajewski.Game.Data;
using UnityEngine;
using UnityEngine.Events;

namespace JGajewski.Entities.Enemies.Data
{
    public class EnemiesWaveEntity : IEntity
    {
        public event UnityAction<Guid, Vector3, EnemyType> OnEnemyEntityCreated;
        public event UnityAction<Guid, int> OnEnemyEntityDestroyed;
        
        public event UnityAction<Guid, Vector3> OnEnemiesWaveMoved;
        
        public event UnityAction<Range> OnEnemiesWaveBordersChanged;
        public event UnityAction<int> OnEnemiesWaveMovedDown;
        
        public event UnityAction OnEnemiesWaveCleared;

        public EnemyEntity this[Guid enemyGuid] => _enemiesInWave[enemyGuid];
        
        public Guid EntityGuid { get; }

        public IReadOnlyList<Guid> EnemiesAbleToShoot => _enemiesAbleToShoot;
        
        public Vector3 WaveCombinedPosition => new Vector3(WavePosition.x + WaveOffset.x, 0f, WavePosition.z + WaveOffset.y);

        public Vector3 WavePosition { get; private set; }
        public Vector2 WaveOffset { get; private set; }

        private float WaveVerticalPosition => _waveVerticalPositions[_waveVerticalPositionIndex];

        private readonly Dictionary<Guid, EnemyEntity> _enemiesInWave;
        private readonly Dictionary<int, List<Guid>> _waveColumns;
        
        private readonly List<Guid> _enemiesAbleToShoot;
        
        private int _leftColumnIndex;
        private int _rightColumnIndex;

        private int _highestColumnHeightIndex;
        private int _waveVerticalPositionIndex;

        private readonly List<float> _waveHorizontalPositions;
        private readonly List<float> _waveVerticalPositions;

        private readonly int _rowsInWave;
        private readonly int _enemiesInRow;
        
        private readonly IReadOnlyList<EnemyTypeSettings> _enemyTypeSettingsList;
        
        public EnemiesWaveEntity(EnemiesSettings enemiesSettings, GameSettings gameSettings)
        {
            EntityGuid = Guid.NewGuid();

            _enemyTypeSettingsList = enemiesSettings.EnemySubTypeSettingsList;

            _rowsInWave = enemiesSettings.RowsInWave;
            _enemiesInRow = enemiesSettings.EnemiesInRow;
            
            var waveHorizontalOffsetRange = Range.OffsetRange(gameSettings.HorizontalGameViewRange,
                enemiesSettings.WaveHorizontalOffset);
            
            var horizontalPositions = waveHorizontalOffsetRange
                .GetPositionsInRange(enemiesSettings.EnemiesInRow);
            
            _waveVerticalPositionIndex = 0;
            _waveVerticalPositions = gameSettings.VerticalPositions;
            
            _waveHorizontalPositions = horizontalPositions;

            WavePosition = new Vector3(0f, 0f, WaveVerticalPosition);

            _enemiesInWave = new Dictionary<Guid, EnemyEntity>();
            _waveColumns = new Dictionary<int, List<Guid>>();

            _enemiesAbleToShoot = new List<Guid>();
        }

        public void CreateWave()
        {
            var randomTypeSettings = _enemyTypeSettingsList.GetRandomItems(_rowsInWave);
            
            for (int columnIndex = 0; columnIndex < _enemiesInRow; columnIndex++)
                _waveColumns.Add(columnIndex, new List<Guid>());
            
            _leftColumnIndex = 0;
            _rightColumnIndex = _enemiesInRow - 1;

            _highestColumnHeightIndex = _rowsInWave - 1;
            
            for (int rowIndex = 0; rowIndex < _rowsInWave; rowIndex++)
            {
                var rowEnemyTypeSettings = randomTypeSettings[rowIndex];
                var verticalPosition = _waveVerticalPositions[rowIndex];
                
                for (int columnIndex = 0; columnIndex < _enemiesInRow; columnIndex++)
                {
                    var enemyEntity = new EnemyEntity(rowEnemyTypeSettings, new Vector2Int(columnIndex, rowIndex));
                    var enemyRealPosition = new Vector3(_waveHorizontalPositions[columnIndex], 0f, verticalPosition); 
                    
                    OnEnemyEntityCreated?.Invoke(enemyEntity.EntityGuid, 
                        enemyRealPosition, enemyEntity.EnemyType);
                    
                    _enemiesInWave.Add(enemyEntity.EntityGuid, enemyEntity);
                    _waveColumns[columnIndex].Add(enemyEntity.EntityGuid);
                    
                    if(rowIndex + 1 == _rowsInWave) _enemiesAbleToShoot.Add(enemyEntity.EntityGuid);
                }
            }
            
            RecalculateWaveHorizontalBorders();
        }

        public void ClearWave()
        {
            foreach (var enemyEntity in _enemiesInWave)
            {
                OnEnemyEntityDestroyed?.Invoke(enemyEntity.Key, 0);
            }
            
            _waveColumns.Clear();
            _enemiesInWave.Clear();
            _enemiesAbleToShoot.Clear();
            
            _waveVerticalPositionIndex = 0;
            WavePosition = new Vector3(0f, 0f, WaveVerticalPosition);
            WaveOffset = Vector2.zero;
            
            OnEnemiesWaveMoved?.Invoke(EntityGuid, WaveCombinedPosition);
        }

        public bool IsEnemyGuidValid(Guid enemyGuid)
        {
            return _enemiesInWave.ContainsKey(enemyGuid);
        }

        public void DestroyEnemyEntity(Guid enemyGuid)
        {
            if (!_enemiesInWave.TryGetValue(enemyGuid, out var enemyEntity)) return;

            var columnIndex = enemyEntity.EnemyColumnPositionIndexes.x;

            _waveColumns[columnIndex].Remove(enemyGuid);
            _enemiesInWave.Remove(enemyGuid);
            
            OnEnemyEntityDestroyed?.Invoke(enemyGuid, enemyEntity.EnemyScore);

            if (_enemiesInWave.Count == 0)
            {
                OnEnemiesWaveCleared?.Invoke();
                return;
            }

            RecalculateWaveVerticalHeight();
            
            if(columnIndex == _leftColumnIndex || columnIndex == _rightColumnIndex)
                RecalculateWaveHorizontalBorders();
        }

        public void OffsetWavePosition(Vector2 offset)
        {
            WaveOffset = offset;
            OnEnemiesWaveMoved?.Invoke(EntityGuid, WaveCombinedPosition);
        }
        
        public void IncrementWaveVerticalPosition()
        {
            _waveVerticalPositionIndex++;
            WavePosition = new Vector3(0f, 0f, WaveVerticalPosition);
            
            OnEnemiesWaveMovedDown?.Invoke(_waveVerticalPositionIndex + _highestColumnHeightIndex);
        }

        private void RecalculateWaveVerticalHeight()
        {
            _highestColumnHeightIndex = 0;
            _enemiesAbleToShoot.Clear();
            
            foreach (var waveColumn in _waveColumns)
            {
                var columnHighestVerticalIndex = -1;
                var columnShootingEntity = Guid.Empty;
                
                var enemiesInColumnCount = waveColumn.Value.Count;
                for (int i = 0; i < enemiesInColumnCount; i++)
                {
                    var enemyEntity = _enemiesInWave[waveColumn.Value[i]];
                    if (enemyEntity.ColumnVerticalIndex > columnHighestVerticalIndex)
                    {
                        columnShootingEntity = enemyEntity.EntityGuid;
                        columnHighestVerticalIndex = enemyEntity.ColumnVerticalIndex;
                    }
                }

                if (_highestColumnHeightIndex < columnHighestVerticalIndex)
                    _highestColumnHeightIndex = columnHighestVerticalIndex;

                if (columnShootingEntity != Guid.Empty)
                    _enemiesAbleToShoot.Add(columnShootingEntity);
            }
        }
        
        private void RecalculateWaveHorizontalBorders()
        {
            for (int columnIndex = _leftColumnIndex; columnIndex < _rightColumnIndex; columnIndex++)
            {
                var enemiesInColumnCount = _waveColumns[columnIndex].Count;
                if(enemiesInColumnCount <= 0) continue;

                _leftColumnIndex = columnIndex;
                break;
            }
            
            for (int columnIndex = _rightColumnIndex; columnIndex >= _leftColumnIndex; columnIndex--)
            {
                var enemiesInColumnCount = _waveColumns[columnIndex].Count;
                if(enemiesInColumnCount <= 0) continue;

                _rightColumnIndex = columnIndex;
                break;
            }

            var horizontalBorders = new Range(_waveHorizontalPositions[_leftColumnIndex], 
                _waveHorizontalPositions[_rightColumnIndex]);

            OnEnemiesWaveBordersChanged?.Invoke(horizontalBorders);
        }
    }
}
