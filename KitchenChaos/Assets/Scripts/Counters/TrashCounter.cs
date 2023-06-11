using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
