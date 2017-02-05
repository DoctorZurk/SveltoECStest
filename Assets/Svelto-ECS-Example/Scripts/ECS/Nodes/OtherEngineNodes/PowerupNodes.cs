using Svelto.ECS.Example.Components.Base;
using Svelto.ECS.Example.Components.Damageable;
using Svelto.ECS.Example.Components.Powerup;

namespace Svelto.ECS.Example.Nodes.Powerup
{
    public class PowerupNode: NodeWithID
    {
        public IPowerupEngineComponent engineComponent;
        public IPowerupTriggerComponent   triggerComponent;
    }

    public class PowerupSpawningNode : NodeWithID
    {
        public IPowerupSpawnerComponent[] spawnerComponents;
    }
}
