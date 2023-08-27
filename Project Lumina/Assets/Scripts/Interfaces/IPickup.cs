using UnityEngine;

namespace ProjectLumina.Interfaces
{
    public interface IPickup
    {
        void Pickup(GameObject target, int amount);
    }
}