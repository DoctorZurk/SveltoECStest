using Svelto.ECS.Example.Components.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Implementers.HUD
{
    public class BulletsSlider : MonoBehaviour, IBulletsSliderComponent
    {
        Slider IBulletsSliderComponent.bulletsSlider { get { return _slider; } }

        void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        Slider _slider;
    }
}
