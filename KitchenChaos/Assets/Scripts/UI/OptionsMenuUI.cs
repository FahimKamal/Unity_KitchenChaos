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
        [SerializeField] private Transform pressToRebindKeyTrans;

        [Space]
        [Header("Key Bindings")]
        [Space]
        #region Key Binding Section

        [SerializeField] private TextMeshProUGUI moveUpText;
        [SerializeField] private TextMeshProUGUI moveDownText;
        [SerializeField] private TextMeshProUGUI moveLeftText;
        [SerializeField] private TextMeshProUGUI moveRightText;
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private TextMeshProUGUI interactAlternateText;
        [SerializeField] private TextMeshProUGUI pauseText;
        [Space]
        [SerializeField] private Button moveUpButton;
        [SerializeField] private Button moveDownButton;
        [SerializeField] private Button moveLeftButton;
        [SerializeField] private Button moveRightButton;
        [SerializeField] private Button interactButton;
        [SerializeField] private Button interactAlternateButton;
        [SerializeField] private Button pauseButton;
        [Space]
        [Header("Gamepad Key Bindings")]
        [Space]
        [SerializeField] private Button gamePadInteractButton;
        [SerializeField] private Button gamePadInteractAlternateButton;
        [SerializeField] private Button gamePadPauseButton;
        
        [SerializeField] private TextMeshProUGUI gamePadInteractText;
        [SerializeField] private TextMeshProUGUI gamePadInteractAlternateText;
        [SerializeField] private TextMeshProUGUI gamePadPauseText;

        #endregion

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

            moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveUp);});
            moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveDown); });
            moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveLeft); });
            moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.MoveRight); });
            interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
            interactAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.InteractAlternate); });
            pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
            
            gamePadInteractButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePadInteract); });
            gamePadInteractAlternateButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePadInteractAlternate); });
            gamePadPauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.GamePadPause); });
        }

        private void Start(){
            KitchenGameManager.Instance.OnGameUnpaused += (sender, args) => Hide();
            UpdateVisual();
            Hide();
            HidePressToRebindKey();
        }

        private void UpdateVisual(){
            soundEffectsText.text ="Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume * 10f);
            musicText.text ="Music: " + Mathf.Round(MusicManager.Instance.GetVolume * 10f);
            
            moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
            moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
            moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
            moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
            interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
            pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
            
            gamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteract);
            gamePadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadInteractAlternate);
            gamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePadPause);
            
        }

        public void Show(){
            gameObject.SetActive(true);
        }

        private void Hide(){
            gameObject.SetActive(false);
        }

        private void ShowPressToRebindKey(){
            pressToRebindKeyTrans.gameObject.SetActive(true);
        }
        
        private void HidePressToRebindKey(){
            pressToRebindKeyTrans.gameObject.SetActive(false);
        }

        private void RebindBinding(GameInput.Binding binding){
            ShowPressToRebindKey();
            GameInput.Instance.RebindBinding(binding, () => {
                HidePressToRebindKey();
                UpdateVisual();
            });
        }
    }
}
