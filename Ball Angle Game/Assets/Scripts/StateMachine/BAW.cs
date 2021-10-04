using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BAW : State
{
    public override State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy)
    {
        agent.SetDestination(myEnemy.position);
        return this;
    }
}
