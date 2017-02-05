using System;
using Svelto.ECS.Example.Nodes.HUD;
using Svelto.ECS.Example.Nodes.Gun;
using Svelto.ECS.Example.Nodes.Powerup;
using Svelto.ECS.Example.Nodes.Player;
using Svelto.ECS.Example.Components.Powerup;
using Svelto.ECS.Example.Observers.Powerup;
using Svelto.Tasks;

namespace Svelto.ECS.Example.Engines.Powerup
{
    public class PowerupEngine : INodesEngine, IStep<PlayerPowerupType>
    {
        public PowerupEngine(Sequencer powerupCollectedSequence)
        {
            _powerupCollectedSequence = powerupCollectedSequence;
            TaskRunner.Instance.Run(new TimedLoopActionEnumerator(Tick));
        }

        public Type[] AcceptedNodes() { return _acceptedNodes; }

        public void Add(INode obj)
        {
            if(obj is HUDNode) _guiNode = obj as HUDNode;
            if(obj is GunNode) _gunNode = obj as GunNode;
            if(obj is PlayerNode) _playerNode = obj as PlayerNode;
            if (obj is PowerupNode)
            {
                _powerupNode = obj as PowerupNode;
                _powerupNode.triggerComponent.powerupCollected += CheckTarget;
            }
        }

        public void Remove(INode obj)
        {
            if (obj is HUDNode) _guiNode = null;
            if (obj is GunNode) _gunNode = null;
            if (obj is PlayerNode) _playerNode = obj as PlayerNode;
            if (obj is PowerupNode)
            {
                _powerupNode.triggerComponent.powerupCollected -= CheckTarget;
                //_powerupNode = obj as PowerupNode;
            }
        }

        void Tick(float deltaSec)
        {
            if (_playerNode == null) return;


        }

        void CheckTarget(int playerID, int powerupID, PlayerPowerupType type)
        {
            if (_playerNode == null)
                return;

            if (playerID == _playerNode.ID)
            {
                //var playerNode = nodesDB.QueryNode<PlayerNode>(playerID);
                //var component = playerNode..targetTriggerComponent;
                //_action = PowerupActions.ammoCollected;
                _powerupCollectedSequence.Next(this, ref type, Condition.always );


            }
        }

        public void Step( ref PlayerPowerupType type, Enum condition)
        {
            switch (type)
            {
                case PlayerPowerupType.Ammo:
                    // usiamo un dispatchOnChange per i bullets
                    _gunNode.gunComponent.numberOfBulletsLeft.value += 50;
                    if (_gunNode.gunComponent.numberOfBulletsLeft.value > 100) _gunNode.gunComponent.numberOfBulletsLeft.value = 100;// cap the value to maxvalue (100)
                    _guiNode.bulletsSliderComponent.bulletsSlider.value = _gunNode.gunComponent.numberOfBulletsLeft.value;
                    break;
                case PlayerPowerupType.Health:
                    _playerNode.healthComponent.currentHealth += 50;
                    if (_playerNode.healthComponent.currentHealth > 100) _playerNode.healthComponent.currentHealth = 100;// cap the value to maxvalue (100)
                    _guiNode.healthSliderComponent.healthSlider.value = _playerNode.healthComponent.currentHealth;
                    break;
                case PlayerPowerupType.Score:
                    _guiNode.scoreComponent.score += 100;
                    break;
            }
        }

        readonly Type[] _acceptedNodes = { typeof(HUDNode), typeof(GunNode), typeof(PlayerNode), typeof(PowerupNode) };

        GunNode _gunNode;
        HUDNode _guiNode;
        PlayerNode _playerNode;
        PowerupNode _powerupNode;
        Sequencer _powerupCollectedSequence;
    }
}

