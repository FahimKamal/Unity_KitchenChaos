using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameScripts
{
    public class TutorialUI : MonoBehaviour{
        [SerializeField] private TextMeshProUGUI keyMoveUpText;
        [SerializeField] private TextMeshProUGUI keyMoveDownText;
        [SerializeField] private TextMeshProUGUI keyMoveLeftText;
        [SerializeField] private TextMeshProUGUI keyMoveRightText;
        [SerializeField] private TextMeshProUGUI keyInteractText;
        [SerializeField] private TextMeshProUGUI keyInteractAltText;
        [SerializeField] private TextMeshProUGUI keyPauseText;
        [SerializeField] private TextMeshProUGUI keyGamePadPauseText;
        [SerializeField] private TextMeshProUGUI keyMoveGamePadText;
        [SerializeField] private TextMeshProUGUI keyGamePadInteractText;
        [SerializeField] private TextMeshProUGUI keyGamePadInteractAltText;

        private void Start(){
            GameInput.Instance.OnBindingRebind += (sender, args) => UpdateVisual();
            KitchenGameManager.Instance.OnStateChanged += (sender, args) => {
                if (KitchenGameManager.Instance.IsCountdownToStartActive){
                    Hide();
                }
            };
            UpdateVisual();
            Show();
        }

        private void UpdateVisual(){
            keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
            keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
            keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
            keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
            keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
            keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
            keyGamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadPause);
            keyGamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteract);
            keyGamePadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteractAlternate);
        }
        
        private void Show() => gameObject.SetActive(true);
        private void Hide() => gameObject.SetActive(false);
    }
}
