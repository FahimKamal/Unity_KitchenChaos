using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScripts
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }

        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        
        [SerializeField] private RecipeListSO recipeList;
        
        private List<RecipeSO> waitingRecipeSOList;
        private float _spawnRecipeTimer;
        private float _spawnRecipeTimerMax = 4f;
        private int waitingRecipesMax = 4;

        private void Awake()
        {
            Instance = this;
            waitingRecipeSOList = new List<RecipeSO>();
        }

        private void Update()
        {
            _spawnRecipeTimer -= Time.deltaTime;
            if (_spawnRecipeTimer <= 0)
            {
                _spawnRecipeTimer = _spawnRecipeTimerMax;

                if (waitingRecipeSOList.Count < waitingRecipesMax)
                {
                    var waitingRecipeSO = recipeList.recipeSOList[Random.Range(0, recipeList.recipeSOList.Count)];
                    waitingRecipeSOList.Add(waitingRecipeSO);
                    
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (int i = 0; i < waitingRecipeSOList.Count; i++)
            {
                var waitingRecipeSO = waitingRecipeSOList[i];

                if (waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
                {
                    var plateContentsMatchesRecipe = true;
                    // has the same number of ingredients
                    foreach (var recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList)
                    {
                        var ingredientFound = false;
                        // Cycling through all ingredients in the recipe
                        foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                        {
                            // Cycling through all ingredients in the plate
                            if (plateKitchenObjectSO == recipeKitchenObjectSO)
                            {
                                // Ingredient matches!
                                ingredientFound = true;
                                break;
                            }
                        }

                        if (!ingredientFound)
                        {
                            // This recipe ingredient was not found on the plate
                            plateContentsMatchesRecipe = false;
                        }
                    }

                    if (plateContentsMatchesRecipe)
                    {
                        // Player delivered the correct recipe!
                        Debug.Log("Delivered " + waitingRecipeSO.recipeName);
                        waitingRecipeSOList.RemoveAt(i);
                        
                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                }
            }
            
            // No Matches found.
            // Player did not delivered a correct recipe.
            Debug.Log("Player did not delivered a correct recipe.");
        }

        public List<RecipeSO> GetWaitingRecipeSOList()
        {
            return waitingRecipeSOList;
        }
    }
}
