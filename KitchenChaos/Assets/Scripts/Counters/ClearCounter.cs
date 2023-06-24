using UnityEngine;

namespace GameScripts {
    public class ClearCounter : BaseCounter {
        public override void Interact(Player player){
            if (!HasKitchenObject()){
                // There is not kitchenObject here.
                if (player.HasKitchenObject()){
                    // Player is carrying something, give it to counter.
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else{
                    Debug.Log("Player not carrying anything.");
                }
            }
            else{
                // There is a kitchenObject on player.
                if (player.HasKitchenObject()){
                    // Player is carrying something.
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)){
                        // player is carrying plate.
                        // Give the kitchenObject to player.
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())){
                            GetKitchenObject().DestroySelf();
                        }
                    }
                    else{
                        // Player is not carrying plate but other object.
                        if (GetKitchenObject().TryGetPlate(out var plate)){
                            // Counter is holding a plate
                            if (plate.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())){
                                // Give object to plate and destroy object on player.
                                player.GetKitchenObject().DestroySelf();
                            }
                        }
                    }
                }
                else{
                    // Player is not carrying anything. So give player the kitchenObject.
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }
    }
}