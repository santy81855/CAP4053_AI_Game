using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadPref();
    }

    // Update is called once per frame
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void Win2()
    {
        PlayerPrefs.SetInt("level2", 1);
        PlayerPrefs.Save();
    }

    public void Win3()
    {
        PlayerPrefs.SetInt("level3", 1);
        PlayerPrefs.Save();
    }

    public void LoadPref()
    {
        if (!PlayerPrefs.HasKey("level2") || !PlayerPrefs.HasKey("level3"))
        {
            PlayerPrefs.SetInt("level2", 0);
            PlayerPrefs.SetInt("level3", 0);
        }
    }

    public void CheckPref()
    {
        Debug.Log(PlayerPrefs.GetInt("level2"));
        Debug.Log(PlayerPrefs.GetInt("level3"));
    }

    public void ResetPref()
    {
        PlayerPrefs.SetInt("level2", 0);
        PlayerPrefs.SetInt("level3", 0);
    }
}
