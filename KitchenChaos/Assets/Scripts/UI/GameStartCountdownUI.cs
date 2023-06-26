using System;
using System.Collections;
using System.Collections.Generic;
using PlasticGui.Help;
using TMPro;
using UnityEngine;

namespace GameScripts
{
    public class GameStartCountdownUI : MonoBehaviour{
        private const string NUMBER_POPUP = "NumberPopup";
        
        [SerializeField] private TextMeshProUGUI countdownText;

        private Animator _animator;
        private int _previousCountdownNumber;

        private void Awake(){
            _animator = GetComponent<Animator>();
        }

        private void Start(){
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
            
            Hide();
        }

        private void KitchenGameManager_OnStateChanged(object sender, EventArgs e){
            if (KitchenGameManager.Instance.IsCountdownToStartActive){
                Show();
            }
            else{
                Hide();
            }
        }

        private void Update(){
            var countdownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownToStartTimer);
            countdownText.text = countdownNumber.ToString();
            if (_previousCountdownNumber != countdownNumber){
                _previousCountdownNumber = countdownNumber;
                _animator.Play(NUMBER_POPUP);
                SoundManager.Instance.PlayCountDownSound();
            }
        }
        
        private void Hide(){
            gameObject.SetActive(false);
        }

        private void Show(){
            gameObject.SetActive(true);
        }
    }
}
