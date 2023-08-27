using ProjectLumina.Data;
using ProjectLumina.Interfaces;
using UnityEngine;

namespace ProjectLumina.Pickups.Strategies
{
    public class XPPickupStrategy : IPickup
    {
        public void Pickup(GameObject target, int amount)
        {
            if (target.TryGetComponent(out Level level))
            {
                level.AddXP(amount);
            }
        }
    }
}