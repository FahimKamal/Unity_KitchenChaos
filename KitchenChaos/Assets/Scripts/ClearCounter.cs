using UnityEngine;

namespace GameScripts
{
    public class ClearCounter : MonoBehaviour
    {
        public void Interact()
        {
            Debug.Log($"Interacted with {gameObject.name}");
        }
    }
}
