using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class CuttingCounter : BaseCounter
    { 
        [SerializeField] private List<CuttingRecipeSO> cuttingRecipeSoList;
        
        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                // There is not kitchenObject here.
                if (player.HasKitchenObject())
                {
                    // Player is carrying something, give it to counter.
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {
                    Debug.Log("Player not carrying anything.");
                }
            }
            else
            {
                // There is a kitchenObject here.
                if (player.HasKitchenObject())
                {
                    Debug.Log("Player carrying something. Can't grab the item now.");
                }
                else
                {
                    // Player is not carrying anything. So give player the kitchenObject.
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (HasKitchenObject())
            {
                // There is a kitchenObject here. destroy it.
                GetKitchenObject().DestroySelf();

                // Instantiate sliced version of that object.
                // KitchenObject.SpawnKitchenObject(cutKitchenObjectSo, this);
            }
        }
    }
}
