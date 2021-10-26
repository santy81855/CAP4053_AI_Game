using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    public void loadCity()
    {
        SceneManager.LoadScene("PrototypeLevel");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
