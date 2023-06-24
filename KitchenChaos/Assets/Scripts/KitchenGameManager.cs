using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class KitchenGameManager : MonoBehaviour {
        
        public static KitchenGameManager Instance{ get; private set; }

        public event EventHandler OnStateChanged;
        
        private enum State {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver
        }

        private State _state;
        private float _waitingToStartTimer = 1f;
        private float _countdownToStartTimer = 3f;
        private float _gamePlayingTimer;
        private readonly float _gamePlayingTimerMax = 10f;


        private void Awake(){
            Instance = this;
            _state = State.WaitingToStart;
        }

        private void Update(){
            switch (_state){
                case State.WaitingToStart:
                    _waitingToStartTimer -= Time.deltaTime;
                    if (_waitingToStartTimer <= 0){
                        _state = State.CountdownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case State.CountdownToStart:
                    _countdownToStartTimer -= Time.deltaTime;
                    if (_countdownToStartTimer <= 0){
                        _state = State.GamePlaying;
                        _gamePlayingTimer = _gamePlayingTimerMax;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case State.GamePlaying:
                    _gamePlayingTimer -= Time.deltaTime;
                    if (_gamePlayingTimer <= 0){
                        _state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case State.GameOver:

                    break;
            }

            Debug.Log(_state);
        }

        public bool IsGamePlaying => _state is State.GamePlaying;
        public bool IsCountdownToStartActive => _state is State.CountdownToStart;
        public bool IsGameOver => _state is State.GameOver;

        public float GetCountDownToStartTimer => _countdownToStartTimer;

        public float GetGamePlayingTimerNormalized => 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
    }
}