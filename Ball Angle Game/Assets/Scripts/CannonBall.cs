using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Capsule")
        {
            Debug.Log("Dead");
            gameObject.transform.Translate(90, 90, 90);
            //Destroy(other.gameObject);
        }
    }
}
