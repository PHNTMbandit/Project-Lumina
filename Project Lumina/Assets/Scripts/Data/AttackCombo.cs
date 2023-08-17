using Micosmo.SensorToolkit;
using UnityEngine;

namespace ProjectLumina.Data
{
    [CreateAssetMenu(fileName = "Attack Combo", menuName = "Project Lumina/Attack Combo", order = 0)]
    public class AttackCombo : ScriptableObject
    {
        [SerializeField]
        private AnimationClip _animationClip;

        [SerializeField]
        private float _attackDistance;

        public float GetClipLength()
        {
            return _animationClip.length;
        }

        public void SetRayAttackDistance(RaySensor2D sensor)
        {
            sensor.Length = _attackDistance;
        }
    }
}
