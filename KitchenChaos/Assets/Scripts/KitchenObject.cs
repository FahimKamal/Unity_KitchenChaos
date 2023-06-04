using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;

        public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    }
}
