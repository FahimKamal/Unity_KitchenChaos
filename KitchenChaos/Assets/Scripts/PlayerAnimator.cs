using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts {
    public class PlayerAnimator : MonoBehaviour {
        [SerializeField] private Player player;

        private const string IsWalking = "IsWalking";

        private Animator _animator;

        private void Awake(){
            _animator = GetComponent<Animator>();
        }


        private void Update(){
            _animator.SetBool(IsWalking, player.IsWalking());
        }
    }
}