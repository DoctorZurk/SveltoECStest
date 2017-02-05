using Svelto.ECS.Example.Components.Enemy;
using Svelto.ECS.Example.Nodes.Enemies;
using Svelto.ECS.Example.Nodes.HUD;
using Svelto.DataStructures;
using System;
using UnityEngine;
using System.Collections;

namespace Svelto.ECS.Example.Engines.Enemies
{
    public class EnemySpawnerEngine : INodesEngine
    {
        private int creepsToSpawn;       

        internal class EnemySpawnerData
        {

            internal float timeLeft;
            internal GameObject enemy;
            internal float spawnTime;
            internal Transform[] spawnPoints;

            internal EnemySpawnerData(IEnemySpawnerComponent spawnerComponent)
            {
                enemy = spawnerComponent.enemyPrefab;
                spawnTime = spawnerComponent.spawnTime;
                spawnPoints = spawnerComponent.spawnPoints;
                timeLeft = spawnTime;
            }
        }

        public EnemySpawnerEngine(Factories.IGameObjectFactory factory, IEntityFactory entityFactory)
        {
            _factory = factory;
            _entityFactory = entityFactory;
            TaskRunner.Instance.Run(IntervaledTick);
        }

        public Type[] AcceptedNodes()
        {
            return _acceptedNodes;
        }

        public void Add(INode obj)
        {
            if (obj is HUDNode)
            {
                _guiNode = obj as HUDNode;
                _guiNode.HUDAnimator.hudAnimator.SetTrigger("NewWave");
            }
            else
            {
                if (obj is EnemySpawningNode)
                {
                    _enemySpawningNode = obj as EnemySpawningNode;

                    var spawnerComponents = _enemySpawningNode.spawnerComponents;

                    for (int i = 0; i < spawnerComponents.Length; i++)
                        _enemiestoSpawn.Add(new EnemySpawnerData(spawnerComponents[i]));

                }
            }
        }

        public void Remove(INode obj)
        {
            //remove is called on context destroyed, in this case the entire engine will be destroyed
        }

        IEnumerator IntervaledTick()
        {
            while (true)
            {
                yield return _waitForSecondsEnumerator;

                if (_enemySpawningNode.creepsLeft == 0)
                {
                    // Initialize new wave
                    _enemySpawningNode.currentWave++;
                    // UI
                    _guiNode.waveComponent.wave = _enemySpawningNode.currentWave;
                    _guiNode.waveMessageComponent.wave = _enemySpawningNode.currentWave;
                    // Internal
                   creepsToSpawn = 10 * _enemySpawningNode.currentWave;
                   _enemySpawningNode.creepsLeft = creepsToSpawn;
                    // Wave message
                    _guiNode.HUDAnimator.hudAnimator.SetTrigger("NewWave");
                }

                for (int i = _enemiestoSpawn.Count - 1; i >= 0; --i)
                {
                    var spawnData = _enemiestoSpawn[i];

                    if (spawnData.timeLeft <= 0.0f && creepsToSpawn > 0)
                    {
                        // Find a random index between zero and one less than the number of spawn points.
                        int spawnPointIndex = UnityEngine.Random.Range(0, spawnData.spawnPoints.Length);

                        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                        var go = _factory.Build(spawnData.enemy);
                        _entityFactory.BuildEntity(go.GetInstanceID(), go.GetComponent<IEntityDescriptorHolder>().BuildDescriptorType());
                        var transform = go.transform;
                        var spawnInfo = spawnData.spawnPoints[spawnPointIndex];

                        transform.position = spawnInfo.position;
                        transform.rotation = spawnInfo.rotation;
                        spawnData.timeLeft = spawnData.spawnTime;
                        creepsToSpawn -= 1;
                    }

                    
                    _guiNode.zombearsComponent.zombears = _enemySpawningNode.creepsLeft;
                    spawnData.timeLeft -= 1.0f;
                    
                }
            }
        }
        HUDNode _guiNode;
        EnemySpawningNode _enemySpawningNode;
        readonly Type[]                     _acceptedNodes = { typeof(EnemySpawningNode), typeof(HUDNode) };
        FasterList<EnemySpawnerData>        _enemiestoSpawn = new FasterList<EnemySpawnerData>();
        Svelto.Factories.IGameObjectFactory _factory;
        IEntityFactory                      _entityFactory;
        Tasks.WaitForSecondsEnumerator      _waitForSecondsEnumerator = new Tasks.WaitForSecondsEnumerator(1);
    }
}
