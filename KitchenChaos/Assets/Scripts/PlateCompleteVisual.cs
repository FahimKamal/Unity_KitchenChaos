using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [Serializable]
        public struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject gameObject;
        }
        
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;

        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

            foreach (var kitchenObjectSoGameObject in kitchenObjectSO_GameObjectList)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(false);
            }
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            kitchenObjectSO_GameObjectList.Find(x => x.kitchenObjectSO == e.kitchenObjectSO).gameObject.SetActive(true);
        }
    }
}
