using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentMoney = 0;
    
    [SerializeField] private TextMeshProUGUI MoneyText;

    public void EarnMoney(int money)
    {
        currentMoney += money;
        MoneyText.text = "Money: " + currentMoney;
    }
    
}
