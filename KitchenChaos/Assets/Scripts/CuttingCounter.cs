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
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // Player carrying something that can be cut.
                        // Player is carrying something, give it to counter.
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                    }
                    else
                    {
                        Debug.Log("This kitchenObject can't be sliced.");
                    }
                    
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
            if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                // There is a kitchenObject here and it can be cut. destroy it.
                var outputKitchenObj = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();

                // Instantiate sliced version of that object.
                KitchenObject.SpawnKitchenObject(outputKitchenObj, this);
            }
        }
        
        private bool  HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            foreach (var recipe in cuttingRecipeSoList)
            {
                if (recipe.input == inputKitchenObjectSo)
                {
                    return true;
                }
            }
            return false;
        }


        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
        {
            foreach (var recipe in cuttingRecipeSoList)
            {
                if (recipe.input == inputKitchenObjectSo)
                {
                    return recipe.output;
                }
            }
            return  null;
        }
    }
}
