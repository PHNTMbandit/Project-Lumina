using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class HUDNeuroglyphIcon : MonoBehaviour
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
            if (sprite != null)
            {
                _tierImage.enabled = true;
                _tierImage.sprite = sprite;
            }
            else
            {
                _tierImage.enabled = false;
            }
        }
    }
}