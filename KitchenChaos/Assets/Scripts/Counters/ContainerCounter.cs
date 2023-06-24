using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class ContainerCounter : BaseCounter {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenObjectSO _kitchenObjectSo;

        public override void Interact(Player player){
            if (!player.HasKitchenObject()){
                // Player is not carrying anything so spawn new object.
                KitchenObject.SpawnKitchenObject(_kitchenObjectSo, player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}