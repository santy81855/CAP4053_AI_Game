using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFireRate : MonoBehaviour
{
    public PlayerManager playerManager;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We hit");
        playerManager.PowerUp(0);
        Destroy(gameObject);
    }
}
