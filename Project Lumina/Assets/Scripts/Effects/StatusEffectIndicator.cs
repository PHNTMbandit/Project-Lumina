using ProjectLumina.Controllers;
using ProjectLumina.UI;
using UnityEngine;

namespace ProjectLumina.Effects
{
    [AddComponentMenu("Effects/Effect Status Effect Indicator")]
    public class StatusEffectIndicator : MonoBehaviour
    {
        public void ShowStatusEffectIndicator(string statusEffectName)
        {
            ObjectPoolController.Instance.GetPooledObject(statusEffectName, transform.position, ObjectPoolController.Instance.transform, true)
                                         .GetComponent<StatusEffectIndicatorUI>()
                                         .ShowIndicator();

        }
    }
}