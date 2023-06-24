using System;
using System.Collections;
using System.Collections.Generic;
using PlasticGui.Help;
using TMPro;
using UnityEngine;

namespace GameScripts
{
    public class GameStartCountdownUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI countdownText;

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
            countdownText.text = Mathf.Ceil(KitchenGameManager.Instance.GetCountDownToStartTimer).ToString();
        }
        
        private void Hide(){
            gameObject.SetActive(false);
        }

        private void Show(){
            gameObject.SetActive(true);
        }
    }
}
