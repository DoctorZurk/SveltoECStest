using System;
using Svelto.ECS.Example.Nodes.HUD;
using Svelto.ECS.Example.Nodes.Enemies;
using Svelto.ECS.Example.Observers.HUD;

namespace Svelto.ECS.Example.Engines.HUD
{
    public class ScoreEngine : INodesEngine, IQueryableNodeEngine
    {
        public ScoreEngine(ScoreOnEnemyKilledObserver scoreOnEnemyKilledObserver)
        {
            scoreOnEnemyKilledObserver.AddAction(AddScore);
        }
        public IEngineNodeDB nodesDB { set; private get; }
        public Type[] AcceptedNodes() { return _acceptedNodes; }

        public void Add(INode obj) { if (obj is HUDNode) _guiNode = obj as HUDNode; } // if(obj is EnemySpawningNode) _enemySpawnerNode = obj as EnemySpawningNode; }
        public void Remove(INode obj) { _guiNode = null; } // _enemySpawnerNode = null; }

        private void AddScore(ref ScoreActions item)
        {
            switch (item)
            {
                case ScoreActions.bunnyKilled:
                    _guiNode.scoreComponent.score += 10;
                    break;
                case ScoreActions.bearKilled:
                    _guiNode.scoreComponent.score += 20;
                    break;
                case ScoreActions.HellephantKilled:
                    _guiNode.scoreComponent.score += 30;
                    break;
            }
            var _enemySpawnerNode = nodesDB.QueryNodes<EnemySpawningNode>();

            _enemySpawnerNode[0].creepsLeft--;
        }

        readonly Type[] _acceptedNodes = { typeof(HUDNode), typeof(EnemySpawningNode) };

        HUDNode _guiNode;
        EnemySpawningNode _enemySpawnerNode;
    }
}


