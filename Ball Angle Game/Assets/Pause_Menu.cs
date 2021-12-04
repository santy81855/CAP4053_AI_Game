using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject disableCursor;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.hasExploded)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            /*if (!GameIsPaused)
            {
                Pause();
            }*/
            Debug.Log(GameIsPaused);
        }
    }

    public void Resume()
    {
        disableCursor.SetActive(true);
        Cursor.visible = (false);
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = (true);
        Cursor.lockState = CursorLockMode.None;
        disableCursor.SetActive(false);
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        //SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
