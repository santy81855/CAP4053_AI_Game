using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public GameObject congrats;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject helpMenu;
    public GameObject levelMenu;
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.Alpha3))
            SetCheat();

        if (Input.GetKey(KeyCode.Alpha4) && Input.GetKey(KeyCode.Alpha5) && Input.GetKey(KeyCode.Alpha6))
            ResetCheat();

        if (Input.GetKey(KeyCode.Alpha0))
            CheckCheat();

    }
    public void SetCheat()
    {
        PlayerPrefs.SetInt("level2", 1);
        PlayerPrefs.SetInt("level3", 1);
        congrats.SetActive(true);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        helpMenu.SetActive(false);
        levelMenu.SetActive(false);
    }

    public void ResetCheat()
    {
        PlayerPrefs.SetInt("level2", 0);
        PlayerPrefs.SetInt("level3", 0);
    }

    public void CheckCheat()
    {
        Debug.Log(PlayerPrefs.GetInt("level2"));
        Debug.Log(PlayerPrefs.GetInt("level3"));
    }
}
