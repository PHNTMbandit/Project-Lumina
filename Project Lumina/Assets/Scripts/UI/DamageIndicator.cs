using TMPro;
using UnityEngine;

namespace ProjectLumina.UI
{
    public class DamageIndicator : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        public void ShowIndicator(string text, Transform origin)
        {
            _text.SetText(text);
        }
    }
}