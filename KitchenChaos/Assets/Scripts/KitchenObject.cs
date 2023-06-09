using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class KitchenObject : MonoBehaviour {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        private IKitchenObjectParent kitchenObjectParent;

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){
            if (this.kitchenObjectParent != null){
                this.kitchenObjectParent.ClearKitchenObject();
            }

            this.kitchenObjectParent = kitchenObjectParent;

            if (this.kitchenObjectParent.HasKitchenObject()){
                Debug.LogError("IKitchenObjectParent already has a kitchenObject.");
            }

            this.kitchenObjectParent.SetKitchenObject(this);

            transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetKitchenObjectParent(){
            return kitchenObjectParent;
        }

        public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;

        public void DestroySelf(){
            kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject plateKitchenObject){
            if (this is PlateKitchenObject){
                plateKitchenObject = this as PlateKitchenObject;
                return true;
            }

            plateKitchenObject = null;
            return false;
        }

        /// <summary>
        /// Spawn kitchen objects on demand. 
        /// </summary>
        /// <param name="kitchenObjectSo">The object to spawn</param>
        /// <param name="kitchenObjectParent">parent of the object.</param>
        /// <returns></returns>
        public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo,
            IKitchenObjectParent kitchenObjectParent){
            var kitchenObjectTransform = Instantiate(kitchenObjectSo.kitchenObjectPrefab);
            var kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

            return kitchenObject;
        }
    }
}