using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInMenu : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        StartCoroutine(DespawnBall());
    }
    IEnumerator DespawnBall()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
