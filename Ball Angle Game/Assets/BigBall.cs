using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : MonoBehaviour
{
    public PlayerManager playerManager;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We hit");
        playerManager.PowerUp(1);
        Destroy(gameObject);
    }
}
