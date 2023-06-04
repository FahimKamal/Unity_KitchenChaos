using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    [CreateAssetMenu()]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform kitchenObjectPrefab;
        public Sprite  kitchenObjectIcon;
        public string  kitchenObjectName;
    }
}
