using UnityEngine;
using UnityEngine.Serialization;

namespace GameScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private GameInput gameInput;

        private bool _isWalking;

        private void Update()
        {
            var inputVector = gameInput.GetMovementVectorNormalized();
            var moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

            // Detect side collisions
            var moveDistance = moveSpeed * Time.deltaTime;
            var playerRadius = 0.5f;
            var playerHeight = 2.0f;
            
            // If CapsuleCast return false then it means player can move forward.
            var Playerposition = transform.position;
            var canMove = !Physics.CapsuleCast(Playerposition, Playerposition + Vector3.up * playerHeight, playerRadius,moveDir,  moveDistance);

            if (!canMove)
            {
                // cannot move towards moveDir
                // Attempt only X movement
                var moveDirX = new Vector3(moveDir.x, 0.0f, 0.0f).normalized;
                canMove = !Physics.CapsuleCast(Playerposition, Playerposition + Vector3.up * playerHeight, playerRadius,moveDirX,  moveDistance);
                if (canMove)
                {
                    // can only move on the X
                    moveDir = moveDirX;
                }
                else // cannot move on the x
                {
                    // Attempt only Z movement
                    var moveDirZ = new Vector3(0.0f, 0.0f, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(Playerposition, Playerposition + Vector3.up * playerHeight, playerRadius,moveDirZ,  moveDistance);
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
            var rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

        public bool IsWalking()
        {
            return _isWalking;
        }

    }
}
