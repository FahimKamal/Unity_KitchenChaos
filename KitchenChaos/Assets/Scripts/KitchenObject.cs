using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        private ClearCounter clearCounter;

        /// <summary>
        /// Place to know which counter is now holding this kitchen object.
        /// </summary>
        public ClearCounter ClearCounter
        {
            get { return clearCounter; }
            set { clearCounter = value; }
        }

        public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    }
}