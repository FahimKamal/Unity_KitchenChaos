using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    [CreateAssetMenu()]
    public class BurningRecipeSO : ScriptableObject
    {
        public string recipeName;
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float burningTimerMax;


    }
}
