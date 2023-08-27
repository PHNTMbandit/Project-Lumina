using ProjectLumina.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectLumina.UI
{
    public class XPBar : MonoBehaviour
    {
        [SerializeField]
        private Level _level;

        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TextMeshProUGUI _text;

        private void Start()
        {
            _level.onXPGain += UpdateUI;

            UpdateUI();
        }

        private void UpdateUI()
        {
            _slider.maxValue = _level.RequiredXP;
            _slider.value = _level.CurrentXP;

            _text.SetText($"{_level.CurrentXP}/{_level.RequiredXP}");
        }
    }
}