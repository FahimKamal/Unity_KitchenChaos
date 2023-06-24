using UnityEngine;

namespace GameScripts
{
    public class ResetStaticDataManager : MonoBehaviour
    {
        private void Awake(){
            CuttingCounter.ResetStaticData();
            BaseCounter.ResetStaticData();
            TrashCounter.ResetStaticData();
        }
    }
}
