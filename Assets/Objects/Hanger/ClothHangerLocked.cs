using UnityEngine;

public class ClothHangerLocked : MonoBehaviour
{
    public GameObject hangerPrefab;
    public int hangerPrice = 300;
    private GameManager _manager;
    
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_manager.currentMoney >= hangerPrice)
            {
                Instantiate(hangerPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                _manager.SpendMoney(hangerPrice);
            }
        }
    }
}
