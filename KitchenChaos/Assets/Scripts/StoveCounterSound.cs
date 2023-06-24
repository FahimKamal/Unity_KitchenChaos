using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class StoveCounterSound : MonoBehaviour {
        [SerializeField] private StoveCounter stoveCounter;
        private AudioSource _audioSource;

        private void Awake(){
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start(){
            stoveCounter.OnStateChanged += OnStoveCounter_StateChanged;
        }

        private void OnStoveCounter_StateChanged(object sender, StoveCounter.OnStateChangedEventArgs e){
            var playSound = e.state is StoveCounter.State.Fried or StoveCounter.State.Frying;

            if (playSound){
                _audioSource.Play();
            }
            else{
                _audioSource.Pause();
            }
        }
    }
}