using System;
using UnityEngine;

namespace GameScripts {
    public class StoveCounterSound : MonoBehaviour {
        [SerializeField] private StoveCounter stoveCounter;
        private AudioSource _audioSource;
        private float _warningSoundTimer;
        private bool _playWarningSound;

        private void Awake(){
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start(){
            stoveCounter.OnStateChanged += OnStoveCounter_StateChanged;
            stoveCounter.OnProgressChanged += OnStoveCounter_ProgressChanged;
        }

        private void OnStoveCounter_ProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e){
            var burnShowProgressAmount = 0.5f;

            _playWarningSound = stoveCounter.IsFried() && e.ProgressNormalized >= burnShowProgressAmount;
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

        private void Update(){
            if (_playWarningSound){
                _warningSoundTimer -= Time.deltaTime;
                if (_warningSoundTimer <= 0){
                    var warningSoundTimerMax = 0.2f;
                    _warningSoundTimer = warningSoundTimerMax;
                    
                    SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
                }
            }
        }
    }
}