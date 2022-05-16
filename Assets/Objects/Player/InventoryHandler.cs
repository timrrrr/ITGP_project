using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryHandler : MonoBehaviour
{
    public int clothesNumber = 0;

    public int clothesCapacity = 2; // because character has 2 hands :)

    public void PickClothes()
    {
        if (clothesNumber < clothesCapacity)
        {
            clothesNumber += 1;
            //Debug.Log(clothesNumber);
        }
    }

    public void RemoveClothes(int Quantity)
    {
        if (clothesNumber >= Quantity)
        {
            clothesNumber -= Quantity;
            //Debug.Log(clothesNumber);
        }
    }
}
