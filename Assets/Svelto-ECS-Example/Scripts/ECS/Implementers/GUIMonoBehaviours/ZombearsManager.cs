using Svelto.ECS.Example.Components.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Svelto.ECS.Example.Implementers.HUD
{
    public class ZombearsManager : MonoBehaviour, IZombearsComponent
    {
        int IZombearsComponent.zombears { get { return _zombears; } set { _zombears = value; _text.text = "Creeps left: " + _zombears; } }

        void Awake ()
        {
            // Set up the reference.
            _text = GetComponent <Text> ();

            // Reset the score.
            _zombears = 0;
        }

        int     _zombears;
        Text    _text;
    }
}
