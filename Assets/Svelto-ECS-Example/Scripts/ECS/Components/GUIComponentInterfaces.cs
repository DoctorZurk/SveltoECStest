using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Components.HUD
{
    public interface IAnimatorHUDComponent: IComponent
    {
        Animator hudAnimator { get; }
    }

    public interface IDamageHUDComponent: IComponent
    {
        Image damageImage { get; }
        float flashSpeed { get; }
        Color flashColor { get; }
    }

    public interface IHealthSliderComponent: IComponent
    {
        Slider healthSlider { get; }
    }

    public interface IScoreComponent: IComponent
    {
        int score { set; get; }
    }
    public interface IAmmoComponent : IComponent
    {
        int ammo { set; get; }
    }
    public interface IBombComponent : IComponent
    {
        int bombs { set; get; }
    }
    public interface IBulletsSliderComponent : IComponent
    {
        Slider bulletsSlider { get; }
    }

    // Waves manager components
    public interface IWaveComponent : IComponent
    {
        int wave { set; get; }
    }

    // Waves manager components
    public interface IWaveMessageComponent : IComponent
    {
        int wave { set; get; }
    }
    public interface IZombearsComponent : IComponent
    {
        int zombears { set; get; }
    }
}
