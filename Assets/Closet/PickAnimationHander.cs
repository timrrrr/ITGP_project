using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PickAnimationHander : MonoBehaviour
{
    [SerializeField] private float indicatorTimer = 0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [SerializeField] private Image indicatorImage = null;
    [SerializeField] private UnityEvent myEvent = null;
    
    private bool playerInside = false;

    

    private void Update()
    {
        if (playerInside)
        {
            indicatorImage.enabled = true;
            indicatorTimer += Time.deltaTime;
            indicatorImage.fillAmount = indicatorTimer;
            if (indicatorTimer >= maxIndicatorTimer)
            {
                indicatorTimer = 0f;
                indicatorImage.fillAmount = indicatorTimer;
                myEvent.Invoke();
            }
        }
        else
        {
            indicatorImage.enabled = false;
            indicatorTimer = 0f;
            indicatorImage.fillAmount = indicatorTimer;
        }
    }

    private void OnWillRenderObject()
    
    {
        throw new NotImplementedException();
    }
}
