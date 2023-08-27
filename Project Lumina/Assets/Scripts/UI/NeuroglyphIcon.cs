using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class NeuroglyphIcon : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Image _tierImage;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetTierImage(Sprite sprite)
        {
            _tierImage.sprite = sprite;
        }
    }
}