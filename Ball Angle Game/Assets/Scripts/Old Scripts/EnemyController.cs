using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This class is subject to change since the Enemy AI is now in its own script.
// Be weary of implementing this until this comment is gone.
public class EnemyController : MonoBehaviour
{
    // Initialize variables.
    public Camera fpsCam;
    public float lookRadius = 10f;
    public float damage = 10f;
    public float range = 100f;
    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.Instance.treasure.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Create a Raycast
        RaycastHit hit;

        // If the raycast hits the enemy, bob & weave, otherwise target the objective.
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Vector3 move = new Vector3(0f, 0f, -1f);
            agent.Move(move);
            Debug.Log(hit.transform.name);
        }
        else
        {
            agent.SetDestination(target.position);
        }

        // Also always face the objective to destroy
        FaceTarget();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
