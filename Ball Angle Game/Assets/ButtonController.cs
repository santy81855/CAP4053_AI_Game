using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ButtonController : MonoBehaviour
{
    public GameObject warning;
    public AudioSource buttonSound;
    public GameObject helpMenu;
    public GameObject nextSlide;
    public GameObject levelMenu;
    public int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        // if (PlayerPrefs.GetInt("level2") == 1)
        // {
        //     mediumButton.SetActive(true);
        // }

        // if (PlayerPrefs.GetInt("level3") == 1)
        // {
        //     hardButton.SetActive(true);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CanPlayerPass()
    {
        if (currentLevel == 2)
        {
            if (PlayerPrefs.GetInt("level2") == 1)
            {
                buttonSound.Play();
                helpMenu.SetActive(true);
                nextSlide.SetActive(true);
                levelMenu.SetActive(false);
            }
            else
            {
                StartCoroutine(DisplayWarning());
            }
        }
        else if (currentLevel == 3)
        {
            if (PlayerPrefs.GetInt("level3") == 1)
            {
                buttonSound.Play();
                helpMenu.SetActive(true);
                nextSlide.SetActive(true);
                levelMenu.SetActive(false);
            }
            else
            {
                StartCoroutine(DisplayWarning());
            }
        }
    }

    IEnumerator DisplayWarning()
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(3);
        warning.SetActive(false);
    }
}
