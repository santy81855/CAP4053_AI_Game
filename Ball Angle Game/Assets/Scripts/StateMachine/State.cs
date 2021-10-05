using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This is the abstract class State() that handles enemy AI functionality.
// States included:
//      -Target -> Chasing towards the objective
//      -BAW -> Bobbing and weaving when the cannon is pointing at the enemy
//      -Attack -> Hitting the objective to destroy it
// Each class must implement RunCurrentState() so we know what the state does.
public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy);
}
