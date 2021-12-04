using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour
{
    // Start is called before the first frame update

    public void GetFlee(Vector3 myvec)
    {
        StartCoroutine(StartFlee(myvec));
    }

    IEnumerator StartFlee(Vector3 myvec)
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(myvec);
        gameObject.GetComponent<StateManager>().enabled = false;
        yield return new WaitForSeconds(3);
        if (gameObject.GetComponent<StateManager>().isEnemyDead == false)
            gameObject.GetComponent<StateManager>().enabled = true;
    }
}
