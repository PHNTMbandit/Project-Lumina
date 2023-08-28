using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.UI.Animations
{
    [AddComponentMenu("UI/Animations/Enlarge UI")]
    public class EnlargeUI : MonoBehaviour
    {
        [BoxGroup("Transform"), SerializeField]
        private Transform[] _transforms;

        [BoxGroup("Tween"), Range(0, 1), SerializeField]
        private float _growSize, _growDuration;

        [BoxGroup("Tween"), EnumPaging, SerializeField]
        private Ease _growEase;

        public void Enlarge()
        {
            foreach (var transform in _transforms)
            {
                transform.DOScale(new Vector3(_growSize, _growSize, _growSize), _growDuration)
                         .SetEase(_growEase)
                         .SetRelative(true);
            }
        }

        public void Shrink()
        {
            foreach (var transform in _transforms)
            {
                transform.DOScale(new Vector3(-_growSize, -_growSize, -_growSize), _growDuration)
                         .SetEase(_growEase)
                         .SetRelative(true);
            }
        }
    }
}