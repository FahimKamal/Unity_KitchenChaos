using UnityEngine;

namespace GameScripts
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO _kitchenObjectSo;
        [SerializeField] private Transform counterTopPoint;
        public void Interact()
        {
            Debug.Log($"Interacted with {gameObject.name}");
            
            var kitchenObjectTransform = Instantiate(_kitchenObjectSo.kitchenObjectPrefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            
            Debug.Log($"Spawned object: {kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().kitchenObjectName}");
        }
    }
}
