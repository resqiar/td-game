using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int moneyPerSecond = 2; // Amount of money added per second
    private int currentMoney = 0; // Current amount of money
    private float moneyTimer = 0f;
    private float moneyDelay = 1f; // Delay between adding money in seconds

    public TMP_Text moneyText;
    public Button recoverButton;
    public float recoverSpellPrice = 100f;
    public TowerManager towerManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if money is greater or equal than 500, show recover health spell
        if (currentMoney >= recoverSpellPrice) {
            showRecoverSpell(true);
        } else {
            showRecoverSpell(false);
        }

        moneyTimer += Time.deltaTime;

        if (moneyTimer > moneyDelay) {
            // Every second, add money 
            AddMoney(moneyPerSecond * Time.deltaTime);

            moneyTimer = 0f;
        }
    }

    void Update() {
        // If tab is pressed and money is >= price of spell
        if (Input.GetKeyDown(KeyCode.Tab) && currentMoney >= recoverSpellPrice){
            // deduct money by price
            currentMoney -= ((int)recoverSpellPrice);

            // Apply recover health of tower
            towerManager.ApplyRecover();

        }
    }

    public void AddMoney(float amount)
    {
        currentMoney += Mathf.CeilToInt(amount);
        moneyText.text = "$ " + currentMoney.ToString();
    }

    void showRecoverSpell(bool state) {
        recoverButton.gameObject.SetActive(state);
    }
}
