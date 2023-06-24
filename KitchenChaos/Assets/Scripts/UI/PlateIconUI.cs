using System;
using UnityEngine;

namespace GameScripts {
    public class PlateIconUI : MonoBehaviour {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private Transform iconTemplate;

        private void Awake(){
            iconTemplate.gameObject.SetActive(false);
        }

        private void Start(){
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender,
            PlateKitchenObject.OnIngredientAddedEventArgs e){
            UpdateVisual();
        }

        private void UpdateVisual(){
            foreach (Transform child in transform){
                if (child == iconTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (var kitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList()){
                var iconTransform = Instantiate(iconTemplate, transform);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjIcon(kitchenObjectSo);
            }
        }
    }
}