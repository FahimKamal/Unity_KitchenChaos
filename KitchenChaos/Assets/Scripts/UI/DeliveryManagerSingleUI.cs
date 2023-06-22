using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeNameText;

        [SerializeField] private Transform iconContainer;
        [SerializeField] private Transform iconTemplate;

        private void Awake()
        {
            iconTemplate.gameObject.SetActive(false);
        }

        public void SetRecipeSO(RecipeSO recipeSO)
        {
            recipeNameText.text = recipeSO.recipeName;

            foreach (Transform child in iconContainer)
            {
                if (child == iconTemplate) continue;
                Destroy(child.gameObject);
            }

            foreach (var kitchenObjectSO in recipeSO.KitchenObjectSOList)
            {
                var iconTransform = Instantiate(iconTemplate, iconContainer);
                iconTransform.gameObject.SetActive(true);
                iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.kitchenObjectIcon;
            }
        }
    }
}
