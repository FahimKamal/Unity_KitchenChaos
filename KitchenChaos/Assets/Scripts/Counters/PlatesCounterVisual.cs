using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;

        private List<GameObject> _plateVisualGameObjectList;

        private void Awake()
        {
            _plateVisualGameObjectList = new List<GameObject>();
        }

        private void Start()
        {
            platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;

            platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
        }

        private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
        {
            Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

            var plateOffsetY = 0.1f;
            plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualGameObjectList.Count, 0);
            _plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
        }

        private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
        {
            var removedPlate = _plateVisualGameObjectList[_plateVisualGameObjectList.Count - 1];
            _plateVisualGameObjectList.Remove(removedPlate);
            Destroy(removedPlate);
        }
    }
}
