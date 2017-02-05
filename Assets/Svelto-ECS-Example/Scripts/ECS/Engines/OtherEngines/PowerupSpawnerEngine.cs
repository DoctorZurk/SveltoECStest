using System;
using UnityEngine;
using System.Collections;
using Svelto.ECS.Example.Nodes.HUD;
using Svelto.ECS.Example.Nodes.Player;
using Svelto.ECS.Example.Nodes.Gun;
using Svelto.ECS.Example.Nodes.Powerup;
using Svelto.ECS.Example.Observers.Powerup;
using Svelto.ECS.Example.Components.Powerup;
using Svelto.DataStructures;

namespace Svelto.ECS.Example.Engines.Powerup
{
    public class PowerupSpawnerEngine : INodesEngine
    {
        internal class PowerupSpawnerData
        {
            internal float timeLeft;
            internal PlayerPowerupType powerupType;
            internal GameObject powerup;
            internal float spawnTime;
            internal Transform[] spawnPoints;

            internal PowerupSpawnerData(IPowerupSpawnerComponent spawnerComponent)
            {
                powerup = spawnerComponent.powerupPrefab;
                spawnTime = spawnerComponent.spawnTime;
                spawnPoints = spawnerComponent.spawnPoints;
                timeLeft = spawnTime;
                powerupType = spawnerComponent.powerupType;
            }
        }

        public PowerupSpawnerEngine(Factories.IGameObjectFactory factory, IEntityFactory entityFactory)
        {
            _factory = factory;
            _entityFactory = entityFactory;
            TaskRunner.Instance.Run(IntervaledTick);
        }

        public Type[] AcceptedNodes() { return _acceptedNodes; }

        public void Add(INode obj) {
            _guiNode = obj as HUDNode;
            var spawnerComponents = (obj as PowerupSpawningNode).spawnerComponents;

            for (int i = 0; i < spawnerComponents.Length; i++)
                _powerupsToSpawn.Add(new PowerupSpawnerData(spawnerComponents[i]));
        }
        public void Remove(INode obj) { _guiNode = null; }

        IEnumerator IntervaledTick()
        {
            while (true)
            {
                yield return _waitForSecondsEnumerator;

                for (int i = _powerupsToSpawn.Count - 1; i >= 0; --i)
                {
                    var spawnData = _powerupsToSpawn[i];

                    if (spawnData.timeLeft <= 0.0f)
                    {
                        // Find a random index between zero and one less than the number of spawn points.
                        int spawnPointIndex = UnityEngine.Random.Range(0, spawnData.spawnPoints.Length);

                        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                        var go = _factory.Build(spawnData.powerup);
                        _entityFactory.BuildEntity(go.GetInstanceID(), go.GetComponent<IEntityDescriptorHolder>().BuildDescriptorType());
                        var transform = go.transform;
                        var spawnInfo = spawnData.spawnPoints[spawnPointIndex];

                        transform.position = spawnInfo.position + new Vector3(0,1f,0);
                        transform.rotation = spawnInfo.rotation;

                        spawnData.timeLeft = spawnData.spawnTime;
                    }

                    spawnData.timeLeft -= 1.0f;
                }
            }
        }



        //readonly Type[] _acceptedNodes = { typeof(HUDNode) };

        HUDNode _guiNode;

        readonly Type[] _acceptedNodes = { typeof(PowerupSpawningNode) };
        FasterList<PowerupSpawnerData> _powerupsToSpawn = new FasterList<PowerupSpawnerData>();
        Svelto.Factories.IGameObjectFactory _factory;
        IEntityFactory _entityFactory;
        Tasks.WaitForSecondsEnumerator _waitForSecondsEnumerator = new Tasks.WaitForSecondsEnumerator(1);
    }
}


