using System;
using UnityEngine;
using Svelto.ECS.Example.Components.Powerup;

namespace Svelto.ECS.Example.Implementers.Powerups
{
    public class PowerupTrigger : MonoBehaviour, IPowerupTriggerComponent, IPowerupEngineComponent
    {
        public GameObject ID { get { return gameObject; } }

        public event Action<int, int, PlayerPowerupType> powerupCollected;

        bool IPowerupTriggerComponent.targetInRange { set { _targetInRange = value; } }
        bool IPowerupEngineComponent.targetInRange { get { return _targetInRange; } }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 11) // player
            { 
                if (powerupCollected != null)
                {
                    PlayerPowerupType type = (PlayerPowerupType)(this.gameObject.layer - 12);
                    powerupCollected(other.gameObject.GetInstanceID(), gameObject.GetInstanceID(), type);
                }
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<AudioSource>().Play(); // wrong, better playing this on a virtual speaker
                Destroy(gameObject, 1f);
            }
        }

        bool _targetInRange;
    }
}
