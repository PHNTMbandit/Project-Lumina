using ProjectLumina.Pickups.Strategies;

namespace ProjectLumina.Pickups
{
    public class XPPickup : Pickup
    {
        public XPPickup()
        {
            pickupType = new XPPickupStrategy();
        }
    }
}