using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : State
{
    // Create states
    public Attack attackState;
    public BAW bawState;

    // Create other checking variables
    public LayerMask whatIsObj;
    public float attackRange;

    public override State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy)
    {
        // Shoot out a RayCast
        RaycastHit hit;
        Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit);

        // Check if the RayCast from the cannon hit the enemy this script is on.
        if (hit.transform == myEnemy)
        {
            // Roll a dice to go to the BAW state, otherwise return this
            return bawState;

        }
        // Check if the enemy is in the attack range of the video games.
        else if (Physics.CheckSphere(transform.position, attackRange, whatIsObj))
        {
            // Go to the Attack state.
            return attackState;
        }
        else
        {
            // Stay on the Target state
            // Insert targeting code.
            // Can potentially put something here that can make movement smarter.

            agent.SetDestination(target.position);
            //FaceTarget(target);
            return this;
        }
    }

    // DEPRECATED
    // FaceTarget() makes the enemy always face the objective.
    // This is only on in target() because when the enemy is BAWing,
    // it should probably look away.
    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
