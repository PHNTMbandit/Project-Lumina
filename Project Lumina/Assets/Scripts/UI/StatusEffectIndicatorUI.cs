using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class StatusEffectIndicatorUI : MonoBehaviour
    {
        [BoxGroup("Show"), MinMaxSlider(0, 10), SerializeField]
        private Vector2 _yMove;

        [BoxGroup("Show"), Range(0, 1), SerializeField]
        private float _moveDuration;

        [BoxGroup("Show"), EnumPaging, SerializeField]
        private Ease _moveEaseType;

        public void ShowIndicator()
        {
            transform.DOMove(_yMove, _moveDuration)
                                .SetEase(_moveEaseType)
                                .SetRelative(true);
        }
    }
}