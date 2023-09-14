using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using UnityEngine;

namespace ProjectLumina.Effects
{
    [AddComponentMenu("Effects/Effect Hit FX")]
    public class HitFX : MonoBehaviour
    {
        public void ShowHitFX(string FXName)
        {
            ObjectPoolController.Instance.GetPooledObject(FXName, transform.position, new Quaternion(0, transform.localScale.x, 0, 0), true);
        }
    }
}