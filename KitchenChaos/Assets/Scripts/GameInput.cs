using UnityEngine;

namespace GameScripts
{
    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
        }

        public Vector2 GetMovementVectorNormalized()
        {

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
            return  inputVector;
        }
    }
}


