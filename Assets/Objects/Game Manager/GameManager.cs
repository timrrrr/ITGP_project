using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentMoney = 0;
    [SerializeField] private TextMeshProUGUI MoneyText;

    public void Start()
    {
        MoneyText.text = "Money: " + currentMoney;
    }

    public void EarnMoney(int money)
    {
        currentMoney += money;
        MoneyText.text = "Money: " + currentMoney;
    }
    
    public void SpendMoney(int money)
    {
        currentMoney -= money;
        MoneyText.text = "Money: " + currentMoney;
    }
    
    
}
