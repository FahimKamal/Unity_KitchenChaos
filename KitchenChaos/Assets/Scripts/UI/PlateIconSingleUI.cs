using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class PlateIconSingleUI : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        
        public void SetKitchenObjIcon(KitchenObjectSO kitchenObjectSO)
        {
            iconImage.sprite = kitchenObjectSO.kitchenObjectIcon;
        }
    }
}
