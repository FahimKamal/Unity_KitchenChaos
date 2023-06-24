using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class StoveCounterVisual : MonoBehaviour {
        [SerializeField] private StoveCounter stoveCounter;

        [SerializeField] private GameObject stoveOnGameObject;
        [SerializeField] private GameObject particlesGameObject;

        private void Start(){
            stoveCounter.OnStateChanged += (sender, args) => {
                var showVisual = args.state == StoveCounter.State.Frying || args.state == StoveCounter.State.Fried;
                stoveOnGameObject.SetActive(showVisual);
                particlesGameObject.SetActive(showVisual);
            };
        }
    }
}