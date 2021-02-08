using JGajewski.Entities.Enemies.Data;
using JGajewski.Entities.Player.Data;
using JGajewski.Entities.Projectile.Data;
using UnityEngine;
using Zenject;

namespace JGajewski.Game.Data
{
    [CreateAssetMenu(menuName = "JGajewski/GameSettingsInstaller", fileName = "GameSettingsInstaller", order = 0)]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameSettings gameSettings;
        [Space]
        [SerializeField] private ProjectileSettings projectileSettings;
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private EnemiesSettings enemiesSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gameSettings);
            
            Container.BindInstance(projectileSettings);
            Container.BindInstance(playerSettings);
            Container.BindInstance(enemiesSettings);
        }
    }
}