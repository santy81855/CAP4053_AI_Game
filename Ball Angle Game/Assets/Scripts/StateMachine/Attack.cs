using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public StateManager stateManagerRef;
    public Target targetState;
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerManager>();
    }
    // Still haven't finished this class yet. Waiting to implement attack feature.
    public override State RunCurrentState(Transform target, NavMeshAgent agent, Camera fpsCam, Transform myEnemy)
    {
        stateManagerRef.enabled = false;
        OnTriggerEnter();

        return targetState;
    }
    void OnTriggerEnter()
    {
        playerManager.LostLevel();
    }
}
