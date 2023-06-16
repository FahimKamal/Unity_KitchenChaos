using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;
        [SerializeField] private List<CuttingRecipeSO> cuttingRecipeSoList;

        private int _cuttingProgress;

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
                        _cuttingProgress = 0;

                        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = (float) _cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                        });
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
                    if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                    {
                        // player is carrying plate.
                        // Give the kitchenObject to player.
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
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
                _cuttingProgress++;
                
                OnCut?.Invoke(this, EventArgs.Empty);
                
                var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = (float) _cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
                
                if (_cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    var outputKitchenObj = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();

                    // Instantiate sliced version of that object.
                    KitchenObject.SpawnKitchenObject(outputKitchenObj, this);
                }
            }
        }
        
        private bool  HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSo);
            return  cuttingRecipeSO != null;
        }


        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
        {
            var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSo);
            return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
        }

        private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO  inputKitchenObjectSo)
        {
            foreach (var recipe in cuttingRecipeSoList)
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
