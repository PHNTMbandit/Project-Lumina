using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class DamageIndicator : MonoBehaviour
    {
        [BoxGroup("Show"), MinMaxSlider(-10, 10), SerializeField]
        private Vector2 _xMove, _yMove;

        [BoxGroup("Show"), Range(0, 1), SerializeField]
        private float _moveDuration;

        [BoxGroup("Show"), EnumPaging, SerializeField]
        private Ease _moveEaseType;

        [BoxGroup("Hide"), Range(0, 1), SerializeField]
        private float _fadeDuration, _fadeDelay;

        [BoxGroup("Hide"), EnumPaging, SerializeField]
        private Ease _fadeEaseType;

        [FoldoutGroup("References"), SerializeField]
        private TextMeshPro _text;

        public void ShowIndicator(string text, Vector2 origin, Vector2 target)
        {
            _text.SetText(text);

            Vector2 direction = origin - target;
            Vector2 distance = new(Random.Range(_xMove.x, _xMove.y), Random.Range(_yMove.x, _yMove.y));
            transform.DOMove(-direction + distance, _moveDuration)
                     .SetEase(_moveEaseType)
                     .SetRelative(true);

            _text.DOFade(0, _moveDuration)
                 .SetEase(_fadeEaseType)
                 .SetDelay(_fadeDelay);
        }
    }
}