using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter _clearCounter;
        [SerializeField] private GameObject visualGameObject;
        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged  += OnSelectedCounterChanged;
        }

        private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == _clearCounter)
            {
                
            }
        }
    }
}
