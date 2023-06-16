using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

        private List<KitchenObjectSO> _kitchenObjectSOList;

        private void Awake()
        {
            _kitchenObjectSOList = new List<KitchenObjectSO>();
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
            {
                // Not a valid ingredient.
                return false;
            }
            if (_kitchenObjectSOList.Contains(kitchenObjectSO))
            {
                // Already has this type.
                return false;
            }
            else
            {
                _kitchenObjectSOList.Add(kitchenObjectSO);
                return true;
            }
        }
    }
}
