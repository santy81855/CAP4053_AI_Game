using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We hit");
        gameManager.PowerUp(1);
        Destroy(gameObject);
    }
}
