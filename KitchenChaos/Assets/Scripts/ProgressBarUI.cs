using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameScripts
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private CuttingCounter cuttingCounter;
        [SerializeField] private Image barImage;

        private void Start()
        {
            cuttingCounter.OnProgressChanged += cuttingCounter_OnProgressChanged;
            barImage.fillAmount = 0f;
            Hide();
        }

        private void cuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
        {
            barImage.fillAmount = e.ProgressNormalized;
            if (e.ProgressNormalized == 0f || e.ProgressNormalized == 1f)
            {
                Hide();
            }
            else
                Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }


        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
