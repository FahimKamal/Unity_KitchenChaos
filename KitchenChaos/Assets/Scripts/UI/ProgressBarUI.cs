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
        [SerializeField] private GameObject hasProgressBarObject;
        [SerializeField] private Image barImage;

        private IHasProgress _hasProgressBar;
        private void Start()
        {
            _hasProgressBar = hasProgressBarObject.GetComponent<IHasProgress>();

            if (_hasProgressBar == null)
            {
                throw new NullReferenceException("GameObject :" + hasProgressBarObject + "doesn't have a component that implements IHasProgress!");
            }
            
            _hasProgressBar.OnProgressChanged += hasProgress_OnProgressChanged;
            barImage.fillAmount = 0f;
            Hide();
        }

        private void hasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
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
