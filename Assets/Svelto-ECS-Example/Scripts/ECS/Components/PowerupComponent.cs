using UnityEngine;

namespace Svelto.ECS.Example.Components.Powerup
{
    public enum PlayerPowerupType
    {
        Ammo,
        Health,
        Score
    }

    public interface IPowerupSpawnerComponent : IComponent
    {
        GameObject powerupPrefab { get; }
        Transform[] spawnPoints { get; }
        float spawnTime { get; }
        PlayerPowerupType powerupType { get; }
    }

    public interface IPowerupEngineComponent : IComponent
    {
        bool targetInRange { get; }
    }

    public interface IPowerupTriggerComponent : IComponent
    {
        event System.Action<int, int, PlayerPowerupType> powerupCollected;
        bool targetInRange { set; }
    }
}
