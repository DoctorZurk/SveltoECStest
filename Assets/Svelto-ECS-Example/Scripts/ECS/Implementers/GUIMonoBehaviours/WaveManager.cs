using Svelto.ECS.Example.Components.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Implementers.HUD
{
    public class WaveManager : MonoBehaviour, IWaveComponent
    {
        int IWaveComponent.wave { get { return _wave; } set { _wave = value; _text.text = "Current Wave: " + _wave; } }

        void Awake ()
        {
            // Set up the reference.
            _text = GetComponent <Text> ();

            // Reset the score.
            _wave = 0;
        }

        int     _wave;
        Text    _text;
    }
}
