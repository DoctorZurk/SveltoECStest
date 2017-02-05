using Svelto.ECS.Example.Components.Powerup;
using UnityEngine;

namespace Svelto.ECS.Example.Implementers.Powerups
{
    public class PowerupSpawner : MonoBehaviour, IPowerupSpawnerComponent
    {
        public GameObject powerup;              // The powerup prefab to be spawned.
        public PlayerPowerupType powerupType;
        public float spawnTime = 3f;            // How long between each spawn.
        public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

        PlayerPowerupType IPowerupSpawnerComponent.powerupType { get { return powerupType; } }
        GameObject IPowerupSpawnerComponent.powerupPrefab { get { return powerup; } }
        float IPowerupSpawnerComponent.spawnTime { get { return spawnTime; } }
        Transform[] IPowerupSpawnerComponent.spawnPoints { get { return spawnPoints; } }
    }
}
