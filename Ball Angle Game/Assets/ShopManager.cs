using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{
    /// <summary>
    /// IMPORTANT!!!
    /// There are two quantity counters that we are keeping track of. The first one is in the shop itself and the other
    /// is at the corner of the screen when the shop is off. In-Shop quantity counter is controlled by the 'ButtonInfo.cs' script.
    /// In-Scene quantity counter is controlled by the quantityDisplay array with indexes 1, 2, 3, and 4 being each powerup.
    /// </summary>



    // Initialize variables
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public TMP_Text coinsText;
    public TMP_Text coinsGameText;
    public TMP_Text q1;
    public TMP_Text q2;
    public TMP_Text q3;
    public TMP_Text q4;
    public TMP_Text[] quantityDisplay;

    // Singleton
    #region Singleton
    private static ShopManager instance;

    public static ShopManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }


        // ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        // Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;

        // Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }
    #endregion

    void Start()
    {
        // Set coins text
        coinsText.text = coins.ToString();
        coinsGameText.text = coins.ToString();


        // Set quantity text
        quantityDisplay = new TMP_Text[5];
        quantityDisplay[1] = q1;
        quantityDisplay[2] = q2;
        quantityDisplay[3] = q3;
        quantityDisplay[4] = q4;
    }

    public void Buy()
    {
        // Find which button was pressed out of the shop.
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // If we have sufficient coins to purchase the item, buy the item.
        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID])
        {
            // Buy and set coins
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];
            coinsText.text = coins.ToString();
            coinsGameText.text = coins.ToString();

            // Increase quantity and set the quantity counters to the correct number.
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID]++;
            quantityDisplay[ButtonRef.GetComponent<ButtonInfo>().itemID].text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID].ToString();
        }
    }

    // Consumes a powerup charge.
    public void ConsumeCharge(int index)
    {
        shopItems[3, index]--;
        quantityDisplay[index].text = shopItems[3, index].ToString();
    }

    public void addCoins()
    {
        coins += 10;
        coinsText.text = coins.ToString();
        coinsGameText.text = coins.ToString();
    }
}
