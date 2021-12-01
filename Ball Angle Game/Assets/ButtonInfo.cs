using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    // Initialize variables
    public int itemID;
    public TMP_Text priceText;
    public TMP_Text quantityText;
    public GameObject shopManager;

    // Set the initial price and quantity of the numbers inside of the shop
    // (most of the time they are only going to be zero).
    void Start()
    {
        priceText.text = "COST: " + shopManager.GetComponent<ShopManager>().shopItems[2, itemID].ToString();
        quantityText.text = shopManager.GetComponent<ShopManager>().shopItems[3, itemID].ToString();
    }

    // Update the quantity in the shop so it always stays consistent with
    // the amount of powerups the player has.
    void Update()
    {
        quantityText.text = shopManager.GetComponent<ShopManager>().shopItems[3, itemID].ToString();
    }
}
