using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectLumina.UI.Animations
{
    [AddComponentMenu("UI/Animations/Fade Text UI")]
    public class FadeTextUI : MonoBehaviour
    {
        [BoxGroup("Transform"), SerializeField]
        private TextMeshProUGUI[] _texts;

        [BoxGroup("Settings"), Range(0, 1), SerializeField]
        private float _startingAlpha;

        [BoxGroup("Tween"), Range(0, 1), SerializeField]
        private float _fadeInDuration, _fadeOutDuration;

        [BoxGroup("Tween"), EnumPaging, SerializeField]
        private Ease _fadeEase;

        private void Awake()
        {
            foreach (TextMeshProUGUI text in _texts)
            {
                text.alpha = _startingAlpha;
            }
        }

        public void FadeIn()
        {
            foreach (TextMeshProUGUI text in _texts)
            {
                text.DORewind();
                text.DOFade(1, _fadeInDuration)
                    .SetEase(_fadeEase);
            }
        }

        public void FadeOut()
        {
            foreach (TextMeshProUGUI text in _texts)
            {
                text.DORewind();
                text.DOFade(0, _fadeOutDuration)
                    .SetEase(_fadeEase);
            }
        }
    }
}