using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class OptionsMenuUI : MonoBehaviour {
        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;
        

        private void Awake(){
            soundEffectsButton.onClick.AddListener(() => {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            musicButton.onClick.AddListener(() => { });
        }

        private void Start(){
            UpdateVisual();
        }

        private void UpdateVisual(){
            soundEffectsText.text ="Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume * 10f);
        }
    }
}
