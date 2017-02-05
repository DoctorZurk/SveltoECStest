using UnityEngine;

namespace Svelto.ECS.Example.Components.Gun
{
    public interface IGunAttributesComponent: IComponent
    {
        float   timeBetweenBullets { get; }
        Ray     shootRay { get; }
        float   range { get; }
        int     damagePerShot { get; }
        float   timer { get; set; }
        Vector3 lastTargetPosition { get; set; }
        DispatchOnChange<int> numberOfBulletsLeft { get; set; }
    }

    public interface IGunHitTargetComponent : IComponent
    {
        DispatchOnSet<bool> targetHit { get; }
    }

    public interface IGunFXComponent: IComponent
    {
        ParticleSystem  particles { get; }
        LineRenderer    line { get; }
        AudioSource     audio { get; }
        Light           light { get; }
        float           effectsDisplayTime { get; }
    }
}
