using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private BaseCounter _baseCounter;
        [SerializeField] private List<GameObject> visualGameObjectArray;

        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += OnSelectedCounterChanged;
        }

        private void OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
        {
            if (e.SelectedCounter == _baseCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            foreach (var obj in visualGameObjectArray)
            {
                obj.SetActive(true);
            }
        }
        
        private void Hide()
        {
            foreach (var obj in visualGameObjectArray)
            {
                obj.SetActive(false);
            }
        }
    }
}