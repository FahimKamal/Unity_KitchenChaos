using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts {
    public class PlatesCounter : BaseCounter {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;

        [SerializeField] private float spawnPlateTimerMax = 4.0f;
        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
        private float _spawnPlateTimer;
        private int _plateSpawnedAmount;
        private int _plateSpawnedAmountMax = 4;

        private void Update(){
            _spawnPlateTimer += Time.deltaTime;
            if (_spawnPlateTimer >= spawnPlateTimerMax){
                _spawnPlateTimer = 0.0f;
                if (KitchenGameManager.Instance.IsGamePlaying && _plateSpawnedAmount < _plateSpawnedAmountMax){
                    _plateSpawnedAmount++;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(Player player){
            if (!player.HasKitchenObject()){
                // Player is empty handed
                if (_plateSpawnedAmount > 0){
                    // There is least one plate here
                    _plateSpawnedAmount--;
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                    OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void InteractAlternate(Player player){ }
    }
}