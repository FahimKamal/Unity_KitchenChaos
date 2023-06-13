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
        private KitchenObject _kitchenObject;

        public virtual void Interact(Player player)
        {
            Debug.LogError("BaseCounter.Interact(); must be implemented.");
        }

        public virtual void InteractAlternate(Player player)
        {
            // Debug.LogError("BaseCounter.InteractAlternate(); must be implemented.");
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            this._kitchenObject = kitchenObject;
        }

        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}
