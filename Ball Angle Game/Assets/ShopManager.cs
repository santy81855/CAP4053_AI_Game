using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public TMP_Text coinsText;
    public TMP_Text q1;
    public TMP_Text q2;
    public TMP_Text[] quantityDisplay;

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





        quantityDisplay = new TMP_Text[3];
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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(quantityDisplay.Length);
        coinsText.text = "Coins: " + coins.ToString();
        quantityDisplay[1] = q1;
        quantityDisplay[2] = q2;
    }

    public void Buy()
    {
        Debug.Log("ITEM PRESSED");
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().itemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID]++;
            coinsText.text = "Coins: " + coins.ToString();     
            ButtonRef.GetComponent<ButtonInfo>().quantityText.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID].ToString();
            quantityDisplay[ButtonRef.GetComponent<ButtonInfo>().itemID].text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().itemID].ToString();
        }
    }

    public void ConsumeCharge(int index)
    {
        shopItems[3, index]--;
        Debug.Log(shopItems[3, index]);
        quantityDisplay[index].text = shopItems[3, index].ToString();
    }

    public void addCoins()
    {
        coins += 10;
        coinsText.text = "Coins: " + coins.ToString();
    }
}
