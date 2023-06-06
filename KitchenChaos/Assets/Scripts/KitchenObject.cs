using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        private IKitchenObjectParent kitchenObjectParent;

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
        {
            if (this.kitchenObjectParent != null)
            {
                this.kitchenObjectParent.ClearKitchenObject();
            }
            this.kitchenObjectParent = kitchenObjectParent;

            if (this.kitchenObjectParent.HasKitchenObject())
            {
                Debug.LogError("IKitchenObjectParent already has a kitchenObject.");
            }
            this.kitchenObjectParent.SetKitchenObject(this);

            transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetKitchenObjectParent()
        {
            return kitchenObjectParent;
        }

        public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    }
}