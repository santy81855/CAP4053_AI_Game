using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Despawn time can be changed from editor
    [SerializeField]
    private float despawnTime = 3f;
    [SerializeField]
    private int hits = 0;
    // Detects if the cannon ball collides with an object that has the Is trigger option selected.
    // Function has been modified to detect any object that has the Is trigger option selected in the unity editor.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == null)
        {
            return;
        }
        if (hits == 0 && other.gameObject == true)//.name == "Capsule")
        {
            Debug.Log("Dead");
            hits++;
            StartCoroutine(deadTarget(other));
        }
    }

    // Delays the despawn of the enemy off 3 seconds intially. Currently the enemy still moves after being hit.
    public IEnumerator deadTarget(Collider other)
    {
        yield return new WaitForSeconds(despawnTime);
        Debug.Log("Waiting 3 seconds");
        Destroy(other.gameObject);
    }
}
