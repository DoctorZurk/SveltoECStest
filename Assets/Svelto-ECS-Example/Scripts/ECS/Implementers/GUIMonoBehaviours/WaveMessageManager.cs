using Svelto.ECS.Example.Components.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Implementers.HUD
{
    public class WaveMessageManager : MonoBehaviour, IWaveMessageComponent
    {
        int IWaveMessageComponent.wave { get { return _wave; } set { _wave = value; _text.text = "Wave   " + _wave+"\nGet Ready!"; } }

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
