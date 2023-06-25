using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class OptionsMenuUI : MonoBehaviour {
        
        public static OptionsMenuUI Instance { get; private set; }
        
        [SerializeField] private Button soundEffectsButton;
        [SerializeField] private Button musicButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private TextMeshProUGUI soundEffectsText;
        [SerializeField] private TextMeshProUGUI musicText;
        

        private void Awake(){
            Instance = this;
            
            soundEffectsButton.onClick.AddListener(() => {
                SoundManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            musicButton.onClick.AddListener(() => {
                MusicManager.Instance.ChangeVolume();
                UpdateVisual();
            });
            closeButton.onClick.AddListener(Hide);
        }

        private void Start(){
            KitchenGameManager.Instance.OnGameUnpaused += (sender, args) => Hide();
            UpdateVisual();
            Hide();
        }

        private void UpdateVisual(){
            soundEffectsText.text ="Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume * 10f);
            musicText.text ="Music: " + Mathf.Round(MusicManager.Instance.GetVolume * 10f);
        }

        public void Show(){
            gameObject.SetActive(true);
        }

        private void Hide(){
            gameObject.SetActive(false);
        }
    }
}
