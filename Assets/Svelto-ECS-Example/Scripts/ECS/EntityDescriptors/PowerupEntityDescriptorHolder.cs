using UnityEngine;
using Svelto.ECS.Example.Nodes.Powerup;
using Svelto.ECS.Example.Nodes.Player;

namespace Svelto.ECS.Example.EntityDescriptors.Powerups
{
    class PowerupEntityDescriptor : EntityDescriptor
    {
        static readonly INodeBuilder[] _nodesToBuild;

        static PowerupEntityDescriptor()
        {
            _nodesToBuild = new INodeBuilder[]
            {
                new NodeBuilder<PowerupNode>(),
                //new NodeBuilder<PlayerTargetNode>(),
            };
        }

        public PowerupEntityDescriptor(IComponent[] componentsImplementor) : base(_nodesToBuild, componentsImplementor)
        { }
    }

    [DisallowMultipleComponent]
    public class PowerupEntityDescriptorHolder : MonoBehaviour, IEntityDescriptorHolder
    {
        public EntityDescriptor BuildDescriptorType(object[] extraImplentors = null)
        {
            return new PowerupEntityDescriptor(GetComponentsInChildren<IComponent>());
        }
    }
}
