using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private LayerMask counterLayerMask;

        private bool _isWalking;
        private Vector3 _lastInteractDir;

        private void Start()
        {
            gameInput.OnInteractAction += GameInputOnInteractAction;
        }

        private void GameInputOnInteractAction(object sender, EventArgs e)
        {
            HandleInteractions();
        }

        private void Update()
        {
            HandleMovement();
            
            // HandleInteractions();
        }

        public bool IsWalking()
        {
            return _isWalking;
        }

        private void HandleInteractions()
        {
            var inputVector = gameInput.GetMovementVectorNormalized();
            var moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

            if (moveDir != Vector3.zero)
            {
                _lastInteractDir  = moveDir;
            }

            var interactDistance = 2.0f;

            if (Physics.Raycast(transform.position, _lastInteractDir, out var raycastHit, interactDistance, counterLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    clearCounter.Interact();
                }
            }

        }

        private void HandleMovement()
        {
            var inputVector = gameInput.GetMovementVectorNormalized();
            var moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

            // Detect side collisions
            var moveDistance = moveSpeed * Time.deltaTime;
            const float playerRadius = 0.5f;
            const float playerHeight = 2.0f;
            
            // If CapsuleCast return false then it means player can move forward.
            var playerPosition = transform.position;
            var canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius,moveDir,  moveDistance);

            if (!canMove)
            {
                // cannot move towards moveDir
                // Attempt only X movement
                var moveDirX = new Vector3(moveDir.x, 0.0f, 0.0f).normalized;
                canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius,moveDirX,  moveDistance);
                if (canMove)
                {
                    // can only move on the X
                    moveDir = moveDirX;
                }
                else // cannot move on the x
                {
                    // Attempt only Z movement
                    var moveDirZ = new Vector3(0.0f, 0.0f, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius,moveDirZ,  moveDistance);
                    if (canMove)
                    {
                        // can only move on the Z
                        moveDir = moveDirZ;
                    }
                    else
                    {
                        // cannot move any direction.
                    }
                }
            }
            

            // Move player
            if (canMove)
            {
                transform.position += moveDir * moveDistance;
            }
            _isWalking = moveDir != Vector3.zero;

            // Rotate player
            const float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

    }
}
