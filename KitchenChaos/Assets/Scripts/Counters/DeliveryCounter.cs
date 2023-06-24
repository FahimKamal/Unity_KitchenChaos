using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class DeliveryCounter : BaseCounter {
        public static DeliveryCounter Instance{ get; private set; }

        private void Awake(){
            Instance = this;
        }

        public override void Interact(Player player){
            if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)){
                // If player is carrying anything with plate accepts it

                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}