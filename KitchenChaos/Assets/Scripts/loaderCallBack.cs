using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class loaderCallBack : MonoBehaviour {
        private bool _isFirstUpdate = true;

        private void Update(){
            if (_isFirstUpdate){
                _isFirstUpdate = false;
                
                Loader.LoaderCallback();
            }
        }
    }
}
