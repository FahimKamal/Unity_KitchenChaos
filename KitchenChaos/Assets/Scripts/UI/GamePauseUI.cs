using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts {
    public class GamePauseUI : MonoBehaviour {

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button optionsButton;

        private void Awake(){
            resumeButton.onClick.AddListener(() => {
                KitchenGameManager.Instance.TogglePauseGame();
            });
            mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
            optionsButton.onClick.AddListener(() => OptionsMenuUI.Instance.Show());
        }

        private void Start(){
            KitchenGameManager.Instance.OnGamePaused += (sender, args) => Show();
            KitchenGameManager.Instance.OnGameUnpaused += (sender, args) => Hide();
            
            Hide();
        }

        private void Show(){
            gameObject.SetActive(true);
            resumeButton.Select();
        }

        private void Hide () => gameObject.SetActive(false);

    }
}