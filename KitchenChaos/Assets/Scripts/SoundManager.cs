using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GameScripts
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClipRefsSO audioClipRefsSO;
        
        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess  += DeliveryManager_OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
            CuttingCounter.OnAnyCut  += CuttingCounter_OnAnyCut;
            Player.Instance.OnPickedSomething += Player_OnPickedSomething;
            BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
            TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        }

        private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
        {
            var trashCounter = sender as TrashCounter;
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        }

        private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
        {
            var baseCounter = sender as BaseCounter;
            PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
        }

        private void Player_OnPickedSomething(object sender, EventArgs e)
        {
            PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
        }

        private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
        {
            var cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        }

        private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
        {
            var deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
        }

        private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
        {
            var deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        }
        
        private void PlaySound(List<AudioClip> audioClipList, Vector3 pos, float volume = 1f)
        {
            PlaySound(audioClipList[UnityEngine.Random.Range(0, audioClipList.Count)], pos, volume);
        }

        private void PlaySound(AudioClip audioClip, Vector3 pos, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, pos, volume);
        }
    }
}
