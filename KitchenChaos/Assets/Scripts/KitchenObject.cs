using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        private ClearCounter clearCounter;

        public void SetClearCounter(ClearCounter counter)
        {
            if (clearCounter != null)
            {
                clearCounter.ClearKitchenObject();
            }
            clearCounter = counter;

            if (clearCounter.HasKitchenObject())
            {
                Debug.LogError("Counter already has a kitchenObject.");
            }
            clearCounter.SetKitchenObject(this);

            transform.parent = clearCounter.GetkitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public ClearCounter GetClearCounter()
        {
            return clearCounter;
        }

        public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    }
}