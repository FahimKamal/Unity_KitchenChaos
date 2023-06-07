using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public interface IKitchenObjectParent
    {
        /// <summary>
        /// Get the location of the kitchenObject hold Point.
        /// </summary>
        /// <returns></returns>
        public Transform GetKitchenObjectFollowTransform();

        /// <summary>
        /// Set new kitchenObject to the counter.
        /// </summary>
        /// <param name="kitchenObject"></param>
        public void SetKitchenObject(KitchenObject kitchenObject);

        /// <summary>
        /// Get the kitchenObject held by counter.
        /// </summary>
        /// <returns></returns>
        public KitchenObject GetKitchenObject();

        /// <summary>
        /// Remove kitchenObject from counter.
        /// </summary>
        public void ClearKitchenObject();

        /// <summary>
        /// Check if counter has any kitchenObject on it. 
        /// </summary>
        /// <returns></returns>
        public bool HasKitchenObject();
    }
}
