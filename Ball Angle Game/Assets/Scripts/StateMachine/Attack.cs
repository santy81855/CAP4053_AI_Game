using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    // Still haven't finished this class yet. Waiting to implement attack feature.
    public override State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy)
    {
        Debug.Log("were in");
        agent.SetDestination(myEnemy.position);
        return this;
    }
}
