using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class SoundManager : MonoBehaviour {
        public static SoundManager Instance{ get; private set; }

        private void Awake(){
            Instance = this;
        }

        [SerializeField] private AudioClipRefsSO audioClipRefsSO;

        private float _volume = 1.0f;

        private void Start(){
            DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
            DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
            Player.Instance.OnPickedSomething += Player_OnPickedSomething;
            BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
            TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        }

        private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e){
            var trashCounter = sender as TrashCounter;
            PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
        }

        private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e){
            var baseCounter = sender as BaseCounter;
            PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
        }

        private void Player_OnPickedSomething(object sender, EventArgs e){
            PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
        }

        private void CuttingCounter_OnAnyCut(object sender, EventArgs e){
            var cuttingCounter = sender as CuttingCounter;
            PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
        }

        private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e){
            var deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
        }

        private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e){
            var deliveryCounter = DeliveryCounter.Instance;
            PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
        }

        private void PlaySound(List<AudioClip> audioClipList, Vector3 pos, float volumeMultiplier = 1f){
            PlaySound(audioClipList[UnityEngine.Random.Range(0, audioClipList.Count)], pos, volumeMultiplier);
        }

        private void PlaySound(AudioClip audioClip, Vector3 pos, float volumeMultiplier = 1f){
            AudioSource.PlayClipAtPoint(audioClip, pos, volumeMultiplier * _volume);
        }

        public void PlayFootSepsSound(Vector3 pos, float vol){
            PlaySound(audioClipRefsSO.footstep, pos, vol);
        }

        public void ChangeVolume(){
            _volume += 0.1f;
            if (_volume > 1.0f) _volume = 1.0f;
        }

        public float GetVolume => _volume;
    }
}