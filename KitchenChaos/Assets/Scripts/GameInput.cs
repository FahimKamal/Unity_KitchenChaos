using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class GameInput : MonoBehaviour
    {
        public Vector2 GetMovementVectorNormalized()
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
            return  inputVector;
        }
    }
}
