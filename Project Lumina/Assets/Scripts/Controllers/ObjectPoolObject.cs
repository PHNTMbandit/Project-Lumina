using UnityEngine;

namespace ProjectLumina.Controllers
{
    public class ObjectPoolObject : MonoBehaviour
    {
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}