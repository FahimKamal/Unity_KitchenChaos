using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class DeliveryCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
            {
                // If player is carrying anything with plate accepts it
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
