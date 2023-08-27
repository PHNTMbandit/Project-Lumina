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
        private TextMeshProUGUI _amount, _levelText;

        private void Start()
        {
            _level.onXPGain += UpdateUI;

            UpdateUI();
        }

        private void UpdateUI()
        {
            _slider.maxValue = _level.RequiredXP;
            _slider.value = _level.CurrentXP;

            _amount.SetText($"{_level.CurrentXP}/{_level.RequiredXP}");
            _levelText.SetText($"LVL {_level.CurrentLevel}");
        }
    }
}