using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameScripts
{
    public class DeliveryManager : MonoBehaviour
    {
        [SerializeField] private RecipeListSO recipeList;
        
        private List<RecipeSO> waitingRecipeSOList;
        private float _spawnRecipeTimer;
        private float _spawnRecipeTimerMax = 4f;
        private int waitingRecipesMax = 4;

        private void Awake()
        {
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
                    Debug.Log(waitingRecipeSO.recipeName);
                    waitingRecipeSOList.Add(waitingRecipeSO);
                }
            }
        }
    }
}
