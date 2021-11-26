using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdoll : MonoBehaviour
{
    public float radius = 3f;
    public GameObject explosionEffect;
    public float force = 2100f;
    Rigidbody[] rigidbodies;
    Collider[] colliders;

    //Rigidbody rb;
    // Start is called before the first frame update
    // Sets the initial conditions for the character with the rigidbodies enabled,
    // coliders disabled and animator on.
    void Start()
    {
        explosionEffect = Resources.Load("DustExplosion") as GameObject;
        Debug.Log(explosionEffect);
        setRigidbodyState(true);
        setColliderState(false);
        GetComponent<Animator>().enabled = true;
    }

    void FixedUpdate()
    {

    }

    // Function is called when the enemy has been shot. Disables animator and makes ragdoll
    public void die(float time)
    {

        GetComponent<Animator>().enabled = false;
        //EnemyBehavior.getAgent().enabled = false;
        setColliderState(true);
        setRigidbodyState(false);
        GetComponent<StateManager>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        Explode();
        // Another check if the gameObject has not been deleted yet
        StartCoroutine(EnemyGone());


    }

    void setRigidbodyState(bool state)
    {
        // Contains all rigidbody limbs
        rigidbodies = GetComponentsInChildren<Rigidbody>();

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
        colliders = GetComponentsInChildren<Collider>();

        // Turns on or off the colliders for each limb
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        // Sets the collider state to the opposite for the parent
        GetComponent<Collider>().enabled = !state;
    }

    // For explosion physics after collision.
    void Explode()
    {
        // Debug.Log("Explode");
        GameObject explosion = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);

        // Debug.Log("Transform: " + transform.position);
        foreach (Rigidbody bodypart in rigidbodies)
        {
            // Debug.Log("body" + bodypart);
            if (bodypart != null)
            {
                // Debug.Log("Adding Force to " + bodypart);
                bodypart.AddExplosionForce(force / 2, transform.position, radius, (float)ForceMode.Impulse);
                bodypart.AddRelativeForce(Vector3.back * 2 * force);
            }
        }

        Destroy(explosion, 2f);
    }
    IEnumerator EnemyGone()
    {
        yield return new WaitForSecondsRealtime(7);
        if (gameObject != null)
        {
            //Destroy(EnemyBehavior());
            Destroy(gameObject);
        }
    }
}
