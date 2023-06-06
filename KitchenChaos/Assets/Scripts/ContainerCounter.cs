using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenObjectSO _kitchenObjectSo;

        public override void Interact(Player player)
        {
            Debug.Log($"Interacted with {gameObject.name}");

            if (!HasKitchenObject())
            {
                var kitchenObjectTransform = Instantiate(_kitchenObjectSo.kitchenObjectPrefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
                
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }

        }
    }
}
