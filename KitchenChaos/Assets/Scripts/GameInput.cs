using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameScripts{
    public class GameInput : MonoBehaviour{
        private const string PLAYER_PREFS_BINDINGS = "InputBindings";
        public static GameInput Instance{ get; private set; }
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        public enum Binding{
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Interact,
            InteractAlternate,
            Pause,
            GamePadInteract,
            GamePadInteractAlternate,
            GamePadPause
        }

        private PlayerInputActions _playerInputActions;

        private void Awake(){
            Instance = this;
            _playerInputActions = new PlayerInputActions();
            
            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)){
                var bindingsString = PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS);
                _playerInputActions.LoadBindingOverridesFromJson(bindingsString);
            }
            
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interact.performed += InteractPerformed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
            _playerInputActions.Player.Pause.performed += Pause_performed;

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)){
                var bindingsString = PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS);
                _playerInputActions.LoadBindingOverridesFromJson(bindingsString);
            }
        }

        private void OnDestroy(){
            _playerInputActions.Player.Interact.performed -= InteractPerformed;
            _playerInputActions.Player.InteractAlternate.performed -= InteractAlternatePerformed;
            _playerInputActions.Player.Pause.performed -= Pause_performed;

            _playerInputActions.Dispose();
        }

        private void Pause_performed(InputAction.CallbackContext obj){
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        private void InteractAlternatePerformed(InputAction.CallbackContext obj){
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        private void InteractPerformed(InputAction.CallbackContext obj){
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized(){
            #region code for old input system

            // var inputVector = Vector2.zero;
            // if (Input.GetKey(KeyCode.W))
            // {
            //     inputVector.y = 1;
            // }
            //
            // if (Input.GetKey(KeyCode.S))
            // {
            //     inputVector.y = -1;
            // }
            //
            // if (Input.GetKey(KeyCode.A))
            // {
            //     inputVector.x = -1;
            // }
            //
            // if (Input.GetKey(KeyCode.D))
            // {
            //     inputVector.x = 1;
            // }

            #endregion

            var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

            // Normalized inputVector for smooth player movement 
            inputVector = inputVector.normalized;
            return inputVector;
        }

        public string GetBindingText(Binding binding){
            switch (binding){
                default:
                case Binding.MoveUp:
                    return _playerInputActions.Player.Move.bindings[1].ToDisplayString();
                case Binding.MoveDown:
                    return _playerInputActions.Player.Move.bindings[2].ToDisplayString();
                case Binding.MoveLeft:
                    return _playerInputActions.Player.Move.bindings[3].ToDisplayString();
                case Binding.MoveRight:
                    return _playerInputActions.Player.Move.bindings[4].ToDisplayString();
                case Binding.Interact:
                    return _playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                case Binding.InteractAlternate:
                    return _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                case Binding.Pause:
                    return _playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                
                // Bindings for Gamepad
                case Binding.GamePadInteract:
                    return _playerInputActions.Player.Interact.bindings[1].ToDisplayString();
                case Binding.GamePadInteractAlternate:
                    return _playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
                case Binding.GamePadPause:
                    return _playerInputActions.Player.Pause.bindings[1].ToDisplayString();
            }
        }

        public void RebindBinding(Binding binding, Action onActionRebound){
            _playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding){
                default:
                case Binding.MoveUp:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.MoveDown:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.MoveLeft:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.MoveRight:
                    inputAction = _playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.InteractAlternate:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = _playerInputActions.Player.Pause;
                    bindingIndex = 0;
                    break;
                
                // Bindings for Gamepad
                case Binding.GamePadInteract:
                    inputAction = _playerInputActions.Player.Interact;
                    bindingIndex = 1;
                    break;
                case Binding.GamePadInteractAlternate:
                    inputAction = _playerInputActions.Player.InteractAlternate;
                    bindingIndex = 1;
                    break;
                case Binding.GamePadPause:
                    inputAction = _playerInputActions.Player.Pause;
                    bindingIndex = 1;
                    break;
            }

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback => {
                    callback.Dispose();
                    _playerInputActions.Player.Enable();
                    onActionRebound();

                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, _playerInputActions.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                }).Start();
        }
    }
}