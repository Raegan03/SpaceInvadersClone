using System;
using JGajewski.Common;
using JGajewski.Entities.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace JGajewski.Entities.Player.Data
{
    public class PlayerEntity : IEntity
    {
        public event UnityAction<int> OnPlayerDamaged;
        
        public event UnityAction<bool> OnPlayerProtectionChanged;

        public event UnityAction<Vector3> OnPlayerMoved;
        
        public Guid EntityGuid { get; }

        public bool HaveProjectile => PlayerProjectile != Guid.Empty;
        public Guid PlayerProjectile { get; private set; }

        public float PlayerProjectileSpeed { get; private set; }
        
        public int PlayerLives { get; private set; }
        
        public Vector3 PlayerPosition { get; private set; }

        public bool PlayerProtected { get; private set; }

        private readonly PlayerSettings _playerSettings;
        
        public PlayerEntity(PlayerSettings playerSettings, Vector3 startPosition)
        {
            _playerSettings = playerSettings;
            
            EntityGuid = Guid.NewGuid();
            PlayerProjectile = Guid.Empty;
            
            PlayerProjectileSpeed = _playerSettings.PlayerProjectileSpeed;
            PlayerLives = _playerSettings.PlayerStartLives;
            
            PlayerPosition = startPosition;
            
            PlayerProtected = false;
        }

        public void TakeDamage()
        {
            if(PlayerProtected) return;
            
            PlayerLives--;
            OnPlayerDamaged?.Invoke(PlayerLives);
        }

        public void SetProtection(bool protection)
        {
            PlayerProtected = protection;
            OnPlayerProtectionChanged?.Invoke(PlayerProtected);
        }
        
        public void SetPlayerPosition(float playerHorizontalPosition, Range playerPositionRange)
        {
            PlayerPosition = new Vector3(playerHorizontalPosition.Clamp(playerPositionRange), PlayerPosition.y, PlayerPosition.z);
            OnPlayerMoved?.Invoke(PlayerPosition);
        }

        public void UpdatePlayerPosition(float direction, Range playerPositionRange)
        {
            PlayerPosition += Vector3.right * direction * _playerSettings.PlayerSpeed * Time.deltaTime;
            PlayerPosition = new Vector3(PlayerPosition.x.Clamp(playerPositionRange), PlayerPosition.y, PlayerPosition.z);
            
            OnPlayerMoved?.Invoke(PlayerPosition);
        }

        public void SetPlayerProjectile(Guid playerProjectile)
        {
            PlayerProjectile = playerProjectile;
        }
        
        public void ClearPlayerProjectile()
        {
            PlayerProjectile = Guid.Empty;
        }
    }
}
