using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFireRate : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We hit");
        gameManager.PowerUp(0);
        Destroy(gameObject);
    }
}
