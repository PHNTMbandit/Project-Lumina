using Cinemachine;
using UnityEngine;

namespace ProjectLumina.Effects
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    [AddComponentMenu("Effects/Effect Camera Shake")]
    public class CameraShake : MonoBehaviour
    {
        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void Shake(float force)
        {
            _impulseSource.GenerateImpulseWithForce(force / 2);
        }
    }
}