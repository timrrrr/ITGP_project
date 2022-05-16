using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Closet : MonoBehaviour
{
    private bool _isPlayerNearby = false;

    private float coolDownTime = 1f;
    private float remainingTime;
    
    [SerializeField] private Image indicatorImage = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerNearby = true;
            InventoryHandler inventory = other.GetComponent<InventoryHandler>();
            if (inventory.clothesNumber < inventory.clothesCapacity)
            {
                StartCoroutine(GiveOutlothes(inventory));
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerNearby = false;

        }
    }

    IEnumerator GiveOutlothes(InventoryHandler inventory)
    {
        while (_isPlayerNearby && inventory.clothesNumber < inventory.clothesCapacity)
        {
            remainingTime = coolDownTime;
            while (remainingTime > 0 && _isPlayerNearby)
            {
                remainingTime -= 0.05f;
                indicatorImage.enabled = true;
                indicatorImage.fillAmount = 1 - (remainingTime / coolDownTime);
                yield return new WaitForSeconds(0.05f);
            }

            if (remainingTime <= 0 && _isPlayerNearby)
            {
                indicatorImage.enabled = false;
                indicatorImage.fillAmount = 0;
                inventory.PickClothes();
            }
            else if (!_isPlayerNearby)
            {
                indicatorImage.enabled = false;
                indicatorImage.fillAmount = 0;
            }
        }
        
        
    }
}
