using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Wall Slide")]
    public class CharacterWallSlide : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool CanWallSlide { get; private set; }

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        private void Awake()
        {
            _sensor.OnDetected.AddListener(delegate { CanWallSlide = true; });
            _sensor.OnLostDetection.AddListener(delegate { CanWallSlide = false; });
        }
    }
}