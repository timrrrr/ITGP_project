using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    
    public Vector3 spawnPoint = new Vector3(0f,1f,11f);
    public int maxCustomersOnMap = 6;
    private int customersOnMap = 0;
    [SerializeField] private GameObject[] customers;
    [SerializeField] private GameObject customerPrefab;
    
    
    void Start()
    {
        StartCoroutine(SpawnerRoutine());
    }

    private IEnumerator SpawnerRoutine()
    {
        while (true)
        {
            customers = GameObject.FindGameObjectsWithTag("Customer");
            customersOnMap = customers.Length;
            if (customersOnMap < maxCustomersOnMap)
            {
                Instantiate(customerPrefab, spawnPoint, Quaternion.identity);
            }
            yield return new WaitForSeconds(5);
        }
        yield return null;
    }

}
