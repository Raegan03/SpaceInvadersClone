using System;
using System.Collections.Generic;
using System.Linq;
using JGajewski.Common;
using JGajewski.Entities.Abstracts.Systems;
using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Enemies.Signals;
using JGajewski.Entities.Projectile.Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JGajewski.Entities.Enemies.Systems
{
    public class EnemiesShootingSystem : EntityUpdateSystem<EnemiesSystemsManager, EnemiesWaveEntity>
    {
        private Dictionary<Guid, Guid> _firedProjectiles;
        private float _shootDelay;
        
        private readonly EnemiesSettings _enemiesSettings;
        
        public EnemiesShootingSystem(EnemiesSettings enemiesSettings)
        {
            _enemiesSettings = enemiesSettings;
        }
        
        public override void Start(EnemiesSystemsManager manager, EnemiesWaveEntity data)
        {
            base.Start(manager, data);

            _firedProjectiles = new Dictionary<Guid, Guid>();
            _shootDelay = 0f;
            
            Manager.SignalBus.Subscribe<ProjectileCreatedSignal>(OnProjectileCreated);
            Manager.SignalBus.Subscribe<ProjectileDestroyedSignal>(OnProjectileDestroyed);
        }

        public override void Stop()
        {
            Manager.SignalBus.Unsubscribe<ProjectileCreatedSignal>(OnProjectileCreated);
            Manager.SignalBus.Unsubscribe<ProjectileDestroyedSignal>(OnProjectileDestroyed);

            _firedProjectiles.Clear();
            _shootDelay = 0f;
            
            base.Stop();
        }

        public override void Update()
        {
            base.Update();

            if (_shootDelay < _enemiesSettings.ShootDelay)
            {
                _shootDelay += Time.deltaTime;
                return;
            }
            
            if(_firedProjectiles.Count >= _enemiesSettings.MaxProjectilesCountAtOnce) return;

            var randomValue = Random.Range(0f, 1f);
            if (randomValue > _enemiesSettings.ShootChance) return;
            
            var enemiesAbleToShoot = Data.EnemiesAbleToShoot.ToList();
            while (enemiesAbleToShoot.Count > 0)
            {
                var randomEnemyAbleToShoot = enemiesAbleToShoot.GetRandomItem();
                enemiesAbleToShoot.Remove(randomEnemyAbleToShoot);
                    
                if(_firedProjectiles.ContainsValue(randomEnemyAbleToShoot)) continue;
                
                var enemyEntity = Data[randomEnemyAbleToShoot];
                Manager.SignalBus.Fire(new EnemyShootSignal(enemyEntity.EntityGuid, enemyEntity.EnemyProjectileSpeed));

                _shootDelay = 0f;
                break;
            }
        }

        private void OnProjectileCreated(ProjectileCreatedSignal s)
        {
            if(!Data.IsEnemyGuidValid(s.OwnerGuid)) return;
            _firedProjectiles.Add(s.EntityGuid, s.OwnerGuid);
        }
        
        private void OnProjectileDestroyed(ProjectileDestroyedSignal s)
        {
            if(!_firedProjectiles.ContainsKey(s.EntityGuid)) return;
            _firedProjectiles.Remove(s.EntityGuid);
        }
    }
}
