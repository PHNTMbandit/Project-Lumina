using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectLumina.Capabilities
{
    public class Lighteable : MonoBehaviour
    {
        [SerializeField]
        private Light2D[] _lights;

        public void TurnOnLights()
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].enabled = true;
            }
        }

        public void TurnOffLights()
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _lights[i].enabled = false;
            }
        }
    }
}