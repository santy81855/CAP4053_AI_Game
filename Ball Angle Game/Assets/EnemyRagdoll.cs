using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdoll : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    // Sets the initial conditions for the character with the rigidbodies enabled,
    // coliders disabled and animator on.
    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
        GetComponent<Animator>().enabled = true;
    }

    // Function is called when the enemy has been shot. Disables animator and makes ragdoll
    public void die(float time)
    {

        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        //EnemyBehavior.getAgent().enabled = false;
        GetComponent<StateManager>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // Another check if the gameObject has not been deleted yet
        StartCoroutine(EnemyGone());


    }

    void setRigidbodyState(bool state)
    {
        // Contains all rigidbody limbs
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        // Loops to turn on or off the limbs depending on if the character has been shot or not
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        // Sets kinematic state to the opposite of Rigidbody for the parent
        GetComponent<Rigidbody>().isKinematic = !state;

    }


    void setColliderState(bool state)
    {
        // Gets colliders for each limb
        Collider[] colliders = GetComponentsInChildren<Collider>();

        // Turns on or off the colliders for each limb
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        // Sets the collider state to the opposite for the parent
        GetComponent<Collider>().enabled = !state;
    }

    // For explosion physics after collision. Not currently working
    /*void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation
            );

            Collider[] colliders = Physics.OverlapSphere(transform.position, 50f);

            foreach (Collider closeObjects in colliders)
            {
                Rigidbody rigidbody = closeObjects.GetComponent<Rigidbody>();
            }

            if (rigidbody != null)
            {
                Rigidbody.AddExplosionForce(500f, transform.position, 50f);
            }

            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }*/
    IEnumerator EnemyGone()
    {
        yield return new WaitForSecondsRealtime(5);
        if (gameObject != null)
        {
            //Destroy(EnemyBehavior());
            Destroy(gameObject);
        }
    }
}
