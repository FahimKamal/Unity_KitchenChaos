using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class PlayerSounds : MonoBehaviour {
        private float _footStepTimer;
        private float _footStepTimerMax = 0.1f;
        private Player _player;

        private void Awake(){
            _player = GetComponent<Player>();
        }

        private void Update(){
            _footStepTimer -= Time.deltaTime;

            if (_footStepTimer < 0f){
                _footStepTimer = _footStepTimerMax;

                if (_player.IsWalking()){
                    var vol = 1.0f;
                    SoundManager.Instance.PlayFootSepsSound(_player.transform.position, vol);
                }
            }
        }
    }
}