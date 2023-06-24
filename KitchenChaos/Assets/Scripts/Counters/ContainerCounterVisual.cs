using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts {
    public class ContainerCounterVisual : MonoBehaviour {
        [SerializeField] private ContainerCounter containerCounter;
        private Animator _animator;
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");

        private void Awake(){
            _animator = GetComponent<Animator>();
        }

        private void Start(){
            containerCounter.OnPlayerGrabbedObject += (sender, args) => { _animator.SetTrigger(OpenClose); };
        }
    }
}