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
    // Detects if the cannon ball collides with an object that calls die function from enemy ragdoll in EnemyRagdoll.cs.
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject);

        EnemyRagdoll enemy = other.transform.GetComponent<EnemyRagdoll>();

        //Checks if enemy has already been shot
        if (enemy != null)
        {
            enemy.die(despawnTime);
        }
    }
}
