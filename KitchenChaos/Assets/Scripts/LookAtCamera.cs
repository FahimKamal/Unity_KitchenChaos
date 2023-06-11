using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LootAtInverted,
            CameraForward,
            CameraForwardInverted
        }
        [SerializeField] private Mode mode;
        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case  Mode.LootAtInverted:
                    var dirFromCamera = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    
            }
        }
    }
}
