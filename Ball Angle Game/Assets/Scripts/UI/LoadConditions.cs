using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadConditions : MonoBehaviour
{
    public GameObject disableCursor;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    void Start()
    {
        Cursor.visible = (false);
        disableCursor.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
