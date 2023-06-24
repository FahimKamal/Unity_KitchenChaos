using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    [CreateAssetMenu()]
    public class CuttingRecipeSO : ScriptableObject {
        public string recipeName;
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax;
    }
}