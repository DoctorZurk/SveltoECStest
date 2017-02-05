using Svelto.ECS.Example.Components.Damageable;
using Svelto.ECS.Example.Nodes.Gun;
using Svelto.ECS.Example.Nodes.HUD;
using System;
using UnityEngine;

namespace Svelto.ECS.Example.Engines.HUD
{
    public class HUDEngine : INodesEngine, IQueryableNodeEngine, IStep<PlayerDamageInfo>// , IStep<DamageInfo>
    {
        public IEngineNodeDB nodesDB { set; private get; }

        public Type[] AcceptedNodes()
        {
            return _acceptedNodes;
        }

        public HUDEngine()
        {
            TaskRunner.Instance.Run(new Tasks.TimedLoopActionEnumerator(Tick));
        }

        public void Add(INode obj)
        {
            if (obj is HUDNode)
                _guiNode = obj as HUDNode;
        }

        public void Remove(INode obj)
        {
            if (obj is HUDNode)
                _guiNode = null;
        }
        
        void Tick(float deltaSec)
        {
            if (_guiNode == null) return;

            var damageComponent = _guiNode.damageImageComponent;
            var damageImage = damageComponent.damageImage;
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, damageComponent.flashSpeed * deltaSec);
            
            // Update bullets HUD through OnChange dispatcher way
            var nodes = nodesDB.QueryNodes<GunNode>();
            var _playerGunNode = nodes[0];
            if (_playerGunNode == null) Debug.LogError("ops"); // return;
            var playerGunComponent = _playerGunNode.gunComponent;
            _guiNode.bulletsSliderComponent.bulletsSlider.value = playerGunComponent.numberOfBulletsLeft.value;


          
        }

        void OnDamageEvent(ref PlayerDamageInfo damaged)
        {
            var damageComponent = _guiNode.damageImageComponent;
            var damageImage = damageComponent.damageImage;
            damageImage.color = damageComponent.flashColor;
            _guiNode.healthSliderComponent.healthSlider.value = nodesDB.QueryNode<HUDDamageEventNode>(damaged.entityDamaged).healthComponent.currentHealth;
        }

        void OnDeadEvent()
        {
            _guiNode.damageImageComponent.damageImage.color = _guiNode.damageImageComponent.flashColor;
            _guiNode.HUDAnimator.hudAnimator.SetTrigger("GameOver");
        }

        public void Step(ref PlayerDamageInfo token, Enum condition)
        {

            if ((DamageCondition)condition == DamageCondition.damage)
                OnDamageEvent(ref token);
            else
            if ((DamageCondition)condition == DamageCondition.dead)
                OnDeadEvent();

        }
        
        readonly Type[] _acceptedNodes = { typeof(HUDNode), typeof(GunNode) };

        HUDNode         _guiNode;
    }
}

