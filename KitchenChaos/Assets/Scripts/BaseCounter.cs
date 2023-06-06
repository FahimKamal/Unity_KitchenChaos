using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private Transform counterTopPoint;

        /// <summary>
        /// Place to know what kind of kitchen object is setting on top of this counter. 
        /// </summary>
        private KitchenObject KitchenObject;
        
        public virtual void Interact(Player player)
        {
            Debug.LogError("BaseCounter.Interact(); must be implemented.");
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this.KitchenObject = kitchenObject;
        }

        public KitchenObject GetKitchenObject()
        {
            return KitchenObject;
        }

        public void ClearKitchenObject()
        {
            KitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return KitchenObject != null;
        }
    }
}
