using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class DamageIndicatorUI : MonoBehaviour
    {
        [BoxGroup("Show"), MinMaxSlider(-10, 10), SerializeField]
        private Vector2 _xMove, _yMove, _criticalXMove, _criticalYMove;

        [BoxGroup("Show"), Range(0, 1), SerializeField]
        private float _moveDuration, _criticalMoveDuration;

        [BoxGroup("Show"), EnumPaging, SerializeField]
        private Ease _moveEaseType, _criticalMoveEaseType;

        [BoxGroup("Hide"), Range(0, 1), SerializeField]
        private float _fadeDuration, _fadeDelay, _criticalFadeDuration, _criticalFadeDelay;

        [BoxGroup("Hide"), EnumPaging, SerializeField]
        private Ease _fadeEaseType, _criticalFadeEaseType;

        [BoxGroup("Text"), Range(0, 10), SerializeField]
        private float _textSize, _criticalTextSize;

        [BoxGroup("Text"), SerializeField]
        private TextMeshPro _text;

        public void ShowIndicator(bool isCritical, string text, Vector2 origin, Vector2 target, Color colour)
        {
            _text.SetText(text);
            _text.color = colour;

            if (isCritical)
            {
                _text.fontSize = _criticalTextSize;

                Vector2 direction = origin - target;
                Vector2 distance = new(Random.Range(_criticalXMove.x, _criticalXMove.y), Random.Range(_criticalYMove.x, _criticalYMove.y));
                transform.DOMove(-direction + distance, _criticalMoveDuration)
                         .SetEase(_criticalMoveEaseType)
                         .SetRelative(true);

                _text.DOFade(0, _criticalFadeDuration)
                     .SetEase(_criticalFadeEaseType)
                     .SetDelay(_criticalFadeDelay);
            }
            else
            {
                _text.fontSize = _textSize;

                Vector2 direction = origin - target;
                Vector2 distance = new(Random.Range(_xMove.x, _xMove.y), Random.Range(_yMove.x, _yMove.y));
                transform.DOMove(-direction + distance, _moveDuration)
                         .SetEase(_moveEaseType)
                         .SetRelative(true);

                _text.DOFade(0, _fadeDuration)
                     .SetEase(_fadeEaseType)
                     .SetDelay(_fadeDelay);
            }
        }
    }
}