using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this; 
    }

    #endregion

    public GameObject player;

    public void CompleteLevel()
    {
        Debug.Log("LEVEL WON!");
        completeLevelUI.SetActive(true);
    }

    public void LostLevel()
    {
        Debug.Log("LEVEL LOST! TRY AGAIN!");
        lostLevelUI.SetActive(true);
    }
    public GameObject completeLevelUI;
    public GameObject lostLevelUI;

}
