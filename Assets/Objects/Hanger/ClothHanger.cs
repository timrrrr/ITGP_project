using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ClothHanger : MonoBehaviour
{

    [SerializeField] private List<Collider> customersNearbyAbleToBuy;
    
    public int clothesNumber = 0;
    public int clothesCapacity = 4;
    
    [FormerlySerializedAs("ClothesModels")] public GameObject[] clothesModels;
    
    private bool _isPlayerNearby = false;

    private void Start()
    {

        StartCoroutine(CheckAvailableCustomersNearby());
        RefreshModels();

    }

    private IEnumerator CheckAvailableCustomersNearby()
    {
        while (true)
        {
            while (customersNearbyAbleToBuy.Count > 0 && clothesNumber > 0)
            {
                //CustomerInteractionHandler(customersNearbyAbleToBuy[0]);
                CustomerGiveClothes(customersNearbyAbleToBuy[0].GetComponent<CustomerAI>());
                customersNearbyAbleToBuy.RemoveAt(0);
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(2);
        }
        yield return null;
    }

    

    
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Customer")
        {
            Debug.Log("out?!");
            for (int i = 0; i < customersNearbyAbleToBuy.Count; i++)
            {
                if (GameObject.ReferenceEquals( customersNearbyAbleToBuy[i].gameObject, other.gameObject))
                {
                    customersNearbyAbleToBuy.RemoveAt(i);
                    Debug.Log("OUTT!!!");
                }
            }
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
        CustomerGiveClothes(customerAI);
        
        // here y the time that the customer was moving to the hanger the hanger became empty so the customer has to start over
        if (customerAI.clothesNumber < customerAI.clothesCapacity && clothesNumber == 0)
        {
            customerAI.StartLookingAgain();
            customersNearbyAbleToBuy.Add(other);
            Debug.Log("add!!!");
            
        }
    }

    private void CustomerGiveClothes(CustomerAI customerAI)
    {
        while (customerAI.clothesNumber < customerAI.clothesCapacity && clothesNumber > 0)
        {
            customerAI.PickClothes();
            clothesNumber -= 1;
            RefreshModels();
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
