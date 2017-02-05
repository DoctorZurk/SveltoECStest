using Svelto.ECS.Example.Components.HUD;
using UnityEngine;

namespace Svelto.ECS.Example.Implementers.HUD
{
    public class AnimatorHUD: MonoBehaviour, IAnimatorHUDComponent
    {
        Animator animator;
        Animator IAnimatorHUDComponent.hudAnimator { get { return animator; } }

        void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}
