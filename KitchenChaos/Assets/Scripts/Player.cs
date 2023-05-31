using UnityEngine;

namespace GameScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 7f;

        private void Update()
        {
            var inputVector = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                inputVector.y = 1;
            }

            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x = 1;
            }

            // Normalized inputVector for smooth player movement 
            inputVector = inputVector.normalized;

            var moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

            // Move player
            transform.position += moveDir * (moveSpeed * Time.deltaTime);

            // Rotate player
            var rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

    }
}
