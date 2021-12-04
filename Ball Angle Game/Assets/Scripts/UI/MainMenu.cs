using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public GameObject disableCursor;
    // Load the first level
    public void PlayLevelOne()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
        Time.timeScale = 0f;*/
        //disableCursor.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLevelTwo()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
        Time.timeScale = 0f;
        //disableCursor.SetActive(true);*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayLevelThree()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
        Time.timeScale = 0f;*/
        //disableCursor.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    // Quit out of the application
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
