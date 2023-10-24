using ProjectLumina.Controllers;
using ProjectLumina.UI;
using UnityEngine;

namespace ProjectLumina.Effects
{
    [AddComponentMenu("Effects/Effect Damage Indicator")]
    public class DamageIndicator : MonoBehaviour
    {
        public void ShowDamageIndicator(float damage, Vector2 origin, Color colour)
        {
            ObjectPoolController.Instance.GetPooledObject("Damage Indicator", transform.position, ObjectPoolController.Instance.transform, true)
                                         .GetComponent<DamageIndicatorUI>()
                                         .ShowIndicator(damage.ToString(), origin, transform.position, colour);

        }
    }
}