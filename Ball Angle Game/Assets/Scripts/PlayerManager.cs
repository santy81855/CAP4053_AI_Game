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
    public CannonController cannon;


    public GameObject completeLevelUI;
    public GameObject lostLevelUI;


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

    public void PowerUp(int powerIndex)
    {
        // POWERUP INDEX
        //-------------------------------
        // 0 -> Fast Fire Rate
        // 1 -> Bigger Cannon Balls
        if (powerIndex == 0)
            StartCoroutine(FastFireRate());
    }

    IEnumerator FastFireRate()
    {
        float temp = cannon.fireRate;
        cannon.fireRate /= 2f;
        yield return new WaitForSecondsRealtime(7);
        cannon.fireRate = temp;
        Debug.Log("HELLO TESTING");
    }
}
