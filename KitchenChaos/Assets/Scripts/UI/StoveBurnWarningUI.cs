using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    public class StoveBurnWarningUI : MonoBehaviour{
        [SerializeField] private StoveCounter stoveCounter;

        private void Start(){
            stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
            Hide();
        }

        private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
            var burnShowProgressAmount = 0.5f;

            bool show = stoveCounter.IsFried() && e.ProgressNormalized >= burnShowProgressAmount;

            if (show){
                Show();
            }else
                Hide();
        }
        
        private void Show(){gameObject.SetActive(true);}

        private void Hide(){ gameObject.SetActive(false); }
    }
}
