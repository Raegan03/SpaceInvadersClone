using JGajewski.Commands;
using JGajewski.Entities.Commands;
using UnityEngine;
using Zenject;

namespace JGajewski.Entities
{
    [CreateAssetMenu(fileName = "EntityCommandsInstaller", menuName = "JGajewski/EntityCommandsInstaller")]
    public class EntityCommandsInstaller : ScriptableObjectInstaller<EntityCommandsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindCommandHandler<DamageEntityCommand, DamageEntityCommandHandler>();
        }
    }
}