using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject playerUI;
    public GameObject shopUI;
    public GameObject disableCursor;
    public bool isEscapeSafe = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.hasExploded && isEscapeSafe == true && !shopUI.activeSelf)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            Debug.Log(GameIsPaused);
        }
    }

    public void Resume()
    {
        disableCursor.SetActive(true);
        Cursor.visible = (false);
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        shopUI.SetActive(false);
        playerUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = (true);
        Cursor.lockState = CursorLockMode.None;
        disableCursor.SetActive(false);
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
