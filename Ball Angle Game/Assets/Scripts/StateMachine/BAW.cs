using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BAW : State
{
    // Initialize variables
    public Target targetState;
    public StateManager stateManagerRef;
    public int maxNumber = 750;
    public override State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy)
    {
        // Start a Coroutine
        StartCoroutine(waiter());

        IEnumerator waiter()
        {
            // Create a vector
            Vector3 myvec;

            // Generate a number to trigger a BAW. This needs to be adjusted based on the level.
            // Keep in mind this runs every frame so 60 times per frame. Need to find a sweet spot.
            if (Random.Range(1, maxNumber) <= 1)
            {
                // Generate a number either 1 or 2
                // If one, set the enemy destination 20f right of itself.
                if (Random.Range(1, 3) == 1)
                {
                    myvec = new Vector3(0.0f, 0.0f, 20.0f);
                    agent.SetDestination(myEnemy.position + myvec);
                }
                // If two, set the enemy destination 20f right of itself.
                else
                {
                    myvec = new Vector3(0.0f, 0.0f, -20.0f);
                    agent.SetDestination(myEnemy.position + myvec);
                }

                // Disable the stateManager script for some time and then renable it to go back to the target state
                stateManagerRef.enabled = false;
                yield return new WaitForSecondsRealtime(3);
                stateManagerRef.enabled = true;
            }
        }

        return targetState;
    }
}
