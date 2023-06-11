using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        [SerializeField] private CuttingCounter containerCounter;
        private Animator _animator;
        private static readonly int Cut = Animator.StringToHash("Cut");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnCut += (sender, args) => 
            {
                _animator.SetTrigger(Cut);
            };
        }
    }
}
