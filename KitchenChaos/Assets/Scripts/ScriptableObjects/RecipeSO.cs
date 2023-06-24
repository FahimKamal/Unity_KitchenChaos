using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    [CreateAssetMenu]
    public class RecipeSO : ScriptableObject {
        public List<KitchenObjectSO> KitchenObjectSOList;
        public string recipeName;
    }
}