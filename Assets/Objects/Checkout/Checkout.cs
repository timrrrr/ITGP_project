using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    public int clothesPrice = 100;
    
    private List<GameObject> _customersWaiting;
    private GameManager _gameManager;
    private bool isPlayerNearby = false;

    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _customersWaiting = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ServeAll();
        }
        else if (other.tag == "Customer")
        {
            AppendCustomer(other);
        }
    }
    
    private void AppendCustomer(Collider other)
    {
        GameObject customer = other.gameObject;
        if (customer.GetComponent<CustomerAI>().clothesNumber > 0)
        {
            
            _customersWaiting.Add(customer);
        }
            
    }
    
    private void ServeAll()
    {
        Debug.Log("serve");
        _gameManager.EarnMoney(_customersWaiting.Count * clothesPrice);
        foreach (var customer in _customersWaiting)
        {
            CustomerAI ai = customer.GetComponent<CustomerAI>();
            ai.WalkAway();
        }
        _customersWaiting = new List<GameObject>();
    }
    
    
    
    
}
