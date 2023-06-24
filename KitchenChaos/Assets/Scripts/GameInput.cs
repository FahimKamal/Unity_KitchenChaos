using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameScripts {
    public class GameInput : MonoBehaviour {
        
        public static GameInput Instance{ get; private set; }
        
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        private PlayerInputActions _playerInputActions;

        private void Awake(){
            Instance = this;
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();

            _playerInputActions.Player.Interact.performed += InteractPerformed;
            _playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
            _playerInputActions.Player.Pause.performed += Pause_performed;
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
    }
}