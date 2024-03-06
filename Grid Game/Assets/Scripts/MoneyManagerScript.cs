using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManagerScript : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private GameObject moneyDisplay;
    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyDisplay();
    }
    public void SubtractMoney(int amount)
    {
        money -= amount;
        UpdateMoneyDisplay();
    }
    public bool SubtractAndCheckMoney(int amount)
    {
        if(money - amount < 0)
        {
            return false;
        }
        money -= amount;
        UpdateMoneyDisplay();
        return true;
    }
    private void UpdateMoneyDisplay()
    {
        moneyDisplay.GetComponent<TextMeshProUGUI>().text = "$" + money;
    }
}
