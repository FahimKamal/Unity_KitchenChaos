using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class StoveCounter : BaseCounter
    {
        [SerializeField] private List<FryingRecipeSO> _fryingRecipeSOList;

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                // There is not kitchenObject here.
                if (player.HasKitchenObject())
                {
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // Player carrying something that can be cut. give it to counter.
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
        
        private bool  HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSo);
            return  fryingRecipeSO != null;
        }


        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSo);
            return fryingRecipeSO != null ? fryingRecipeSO.output : null;
        }

        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO  inputKitchenObjectSo)
        {
            foreach (var recipe in _fryingRecipeSOList)
            {
                if (recipe.input == inputKitchenObjectSo)
                {
                    return recipe;
                }
            }
            return  null;
        }
    }
}
