using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class HUDNeuroglyphIcon : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}