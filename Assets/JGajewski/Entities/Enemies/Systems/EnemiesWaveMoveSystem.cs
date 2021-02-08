using System;
using JGajewski.Common;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Game.Data;
using UnityEngine;

namespace JGajewski.Entities.Enemies.Systems
{
    public enum EnemiesMoveDirection
    {
        None = 0,
        Left = 1,
        Right = 2,
        Down = 3
    }
    
    public class EnemiesWaveMoveSystem : EntityUpdateSystem<EnemiesSystemsManager, EnemiesWaveEntity>
    {
        private EnemiesMoveDirection _previousEnemiesMoveDirection;
        private EnemiesMoveDirection _enemiesMoveDirection;

        private float _currentHorizontalOffset;
        private float _currentVerticalOffset;

        private Range _horizontalMoveOffsetRange;
        private Range _verticalMoveOffsetRange;
        
        private readonly EnemiesSettings _enemiesSettings;
        private readonly GameSettings _gameSettings;

        public EnemiesWaveMoveSystem(GameSettings gameSettings, EnemiesSettings enemiesSettings)
        {
            _enemiesSettings = enemiesSettings;
            _gameSettings = gameSettings;
        }

        public override void Start(EnemiesSystemsManager enemiesController, EnemiesWaveEntity data)
        {
            base.Start(enemiesController, data);
            
            Data.OnEnemiesWaveBordersChanged += OnEnemiesWaveBordersChanged;
            Data.OnEnemiesWaveMoved += OnEnemiesWaveMoved;
            Data.OnEnemiesWaveCleared += ClearOffset;

            ClearOffset();
        }

        public override void Stop()
        {
            Data.OnEnemiesWaveBordersChanged -= OnEnemiesWaveBordersChanged;
            Data.OnEnemiesWaveMoved -= OnEnemiesWaveMoved;
            Data.OnEnemiesWaveCleared -= ClearOffset;
            
            base.Stop();
        }
        
        private void OnEnemiesWaveBordersChanged(Range waveBorder)
        {
            _horizontalMoveOffsetRange = Range.OffsetBetweenRanges(
                _gameSettings.HorizontalGameViewRange, waveBorder);
        }
        
        private void OnEnemiesWaveMoved(Guid enemyGuid, Vector3 enemiesWavePosition)
        {
            Manager.SignalBus.Fire(new EnemiesWaveMovedSignal(enemyGuid, enemiesWavePosition));
        }

        public override void Update()
        {
            base.Update();
            
            switch (_enemiesMoveDirection)
            {
                case EnemiesMoveDirection.None:
                    return;
                case EnemiesMoveDirection.Left:
                    _currentHorizontalOffset += _enemiesSettings.HorizontalMoveSpeed * Time.deltaTime;
                    _currentHorizontalOffset = _currentHorizontalOffset.Clamp(_horizontalMoveOffsetRange);

                    if (_currentHorizontalOffset == _horizontalMoveOffsetRange.max)
                        BordersReached();
                    
                    break;
                case EnemiesMoveDirection.Right:
                    _currentHorizontalOffset -= _enemiesSettings.HorizontalMoveSpeed * Time.deltaTime;
                    _currentHorizontalOffset = _currentHorizontalOffset.Clamp(_horizontalMoveOffsetRange);
                    
                    if (_currentHorizontalOffset == _horizontalMoveOffsetRange.min)
                        BordersReached();
                    
                    break;
                case EnemiesMoveDirection.Down:
                    _currentVerticalOffset += _enemiesSettings.VerticalMoveSpeed * Time.deltaTime;
                    _currentVerticalOffset = _currentVerticalOffset.Clamp(_verticalMoveOffsetRange);
                    
                    if (_currentVerticalOffset == _verticalMoveOffsetRange.max)
                        BordersReached();
                    
                    break;
            }
            
            Data.OffsetWavePosition(new Vector2(_currentHorizontalOffset, _currentVerticalOffset));
        }

        private void ClearOffset()
        {
            _horizontalMoveOffsetRange = _enemiesSettings.WaveHorizontalOffset;
            _verticalMoveOffsetRange = new Range(-_gameSettings.VerticalPositionStep, 0f);

            _currentHorizontalOffset = 0f;
            _currentVerticalOffset = 0f;

            _previousEnemiesMoveDirection = EnemiesMoveDirection.Right;
            _enemiesMoveDirection = EnemiesMoveDirection.Right;
        }

        private void BordersReached()
        {
            var cachedPreviousDirection = _enemiesMoveDirection;
            switch (_enemiesMoveDirection)
            {
                case EnemiesMoveDirection.Left:
                case EnemiesMoveDirection.Right:
                    _enemiesMoveDirection = EnemiesMoveDirection.Down;
                    _currentVerticalOffset = _verticalMoveOffsetRange.min;
                    
                    Data.IncrementWaveVerticalPosition();
                    break;
                case EnemiesMoveDirection.Down:
                    switch (_previousEnemiesMoveDirection)
                    {
                        case EnemiesMoveDirection.Left:
                            _enemiesMoveDirection = EnemiesMoveDirection.Right;
                            break;
                        case EnemiesMoveDirection.Right:
                            _enemiesMoveDirection = EnemiesMoveDirection.Left;
                            break;
                    }
                    break;
            }
            _previousEnemiesMoveDirection = cachedPreviousDirection;
        }
    }
}
