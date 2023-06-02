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

            // Move player
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
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
