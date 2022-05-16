using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClothHanger : MonoBehaviour
{
    
    
    public int clothesNumber = 0;
    public int clothesCapacity = 4;
    
    [FormerlySerializedAs("ClothesModels")] public GameObject[] clothesModels;
    
    private bool _isPlayerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInteractionHandler(other);
        }
        else if (other.tag == "Customer")
        {
            CustomerInteractionHandler(other);
        }
        
        
    }

    private void PlayerInteractionHandler(Collider other)
    {
        InventoryHandler playerInventory = other.GetComponent<InventoryHandler>();
        if (playerInventory.clothesNumber > 0 && clothesNumber < clothesCapacity)
        {
            int howManyToPut = Math.Min(clothesCapacity - clothesNumber, playerInventory.clothesNumber);
            playerInventory.RemoveClothes(howManyToPut);
            clothesNumber += howManyToPut;
            RefreshModels();
        }
    }

    private void CustomerInteractionHandler(Collider other)
    {
        CustomerAI customerAI = other.GetComponent<CustomerAI>();
        while (customerAI.clothesNumber < customerAI.clothesCapacity && clothesNumber > 0)
        {
            customerAI.PickClothes();
            clothesNumber -= 1;
            RefreshModels();
        }
        // here y the time that the customer was moving to the hanger the hanger became empty so the customer has to start over
        if (customerAI.clothesNumber < customerAI.clothesCapacity && clothesNumber == 0)
        {
            customerAI.StartLookingAgain();
        }
        
    }



    private void RefreshModels()
    {
        for (int i = 0; i < clothesModels.Length; i++)
        {
            if (i < clothesNumber)
                clothesModels[i].SetActive(true);
            else
                clothesModels[i].SetActive(false);
        }
    }
}
