using ProjectLumina.Interfaces;
using UnityEngine;

namespace ProjectLumina.Pickups
{
    public abstract class Pickup : MonoBehaviour
    {
        [SerializeField]
        private int _amount;

        protected IPickup pickupType;

        public void TryPickup(GameObject target)
        {
            pickupType?.Pickup(target, _amount);

            gameObject.SetActive(false);
        }
    }
}