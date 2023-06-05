using System;
using UnityEngine;

namespace GameScripts
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private ClearCounter secondClearCounter;
        [SerializeField] private bool testing;

        /// <summary>
        /// Place to know what kind of kitchen object is setting on top of this counter. 
        /// </summary>
        private KitchenObject _kitchenObject;

        private void Update()
        {
            if (testing && Input.GetKeyDown(KeyCode.T))
            {
                if (_kitchenObject != null)
                {
                    _kitchenObject.SetClearCounter(secondClearCounter);
                }
            }
        }

        public void Interact()
        {
            Debug.Log($"Interacted with {gameObject.name}");

            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(_kitchenObjectSo.kitchenObjectPrefab, counterTopPoint);
                kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);

                Debug.Log(
                    $"Spawned object: {kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().kitchenObjectName}");
            }
            else
            {
                Debug.Log($"Already have object: {_kitchenObject.GetKitchenObjectSO().kitchenObjectName}");
            }
        }
        
        public Transform GetkitchenObjectFollowTransform()
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