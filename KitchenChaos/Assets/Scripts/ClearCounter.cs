using UnityEngine;

namespace GameScripts
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private ClearCounter secondClearCounter;
        
        /// <summary>
        /// Place to know what kind of kitchen object is setting on top of this counter. 
        /// </summary>
        private KitchenObject _kitchenObject;
        
        public void Interact()
        {
            Debug.Log($"Interacted with {gameObject.name}");

            if (_kitchenObject == null)
            {
                var kitchenObjectTransform = Instantiate(_kitchenObjectSo.kitchenObjectPrefab, counterTopPoint);
                kitchenObjectTransform.localPosition = Vector3.zero;
            
                Debug.Log($"Spawned object: {kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().kitchenObjectName}");
                _kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
                _kitchenObject.ClearCounter = this;
            }
            else
            {
                Debug.Log($"Already have object: {_kitchenObject.GetKitchenObjectSO().kitchenObjectName}");
            }
        }
    }
}
