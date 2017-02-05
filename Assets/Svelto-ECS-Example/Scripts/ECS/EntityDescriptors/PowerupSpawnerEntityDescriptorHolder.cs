using UnityEngine;
using Svelto.ECS.Example.Nodes.Powerup;
using Svelto.DataStructures;
using System;
using Svelto.ECS.Example.Components.Powerup;

namespace Svelto.ECS.Example.EntityDescriptors.PowerupSpawner
{
    class PowerupSpawnerEntityDescriptor : EntityDescriptor
     {
         IPowerupSpawnerComponent[] _components;

        public PowerupSpawnerEntityDescriptor(IPowerupSpawnerComponent[] componentsImplementor):base(null, componentsImplementor)
		{
             _components = componentsImplementor;
        }

        public override FasterList<INode> BuildNodes(int ID, Action<INode> removeAction)
        {
            var nodes = new FasterList<INode>();
            var node = new PowerupSpawningNode
            {
                spawnerComponents = _components
            };

            nodes.Add(node);
            return nodes;
        }
     }

	[DisallowMultipleComponent]
	public class PowerupSpawnerEntityDescriptorHolder : MonoBehaviour, IEntityDescriptorHolder
    {
		public EntityDescriptor BuildDescriptorType(object[] extraImplentors = null)
		{
			return new PowerupSpawnerEntityDescriptor(GetComponents<IPowerupSpawnerComponent>());
		}
	}
}

