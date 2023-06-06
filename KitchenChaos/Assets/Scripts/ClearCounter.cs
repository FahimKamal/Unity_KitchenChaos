using System;
using UnityEngine;

namespace GameScripts
{
    public class ClearCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;
        [SerializeField] private Transform counterTopPoint;

        /// <summary>
        /// Place to know what kind of kitchen object is setting on top of this counter. 
        /// </summary>
        private KitchenObject _kitchenObject;

        public void Interact(Player player)
        {
            Debug.Log($"Interacted with {gameObject.name}");

            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(_kitchenObjectSo.kitchenObjectPrefab, counterTopPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);

                Debug.Log(
                    $"Spawned object: {kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().kitchenObjectName}");
            }
            else
            {
                // Give the object to the player
                _kitchenObject.SetKitchenObjectParent(player);
            }
        }

        public Transform GetKitchenObjectFollowTransform()
        {
            return  counterTopPoint;
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