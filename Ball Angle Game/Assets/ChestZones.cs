using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChestZones : MonoBehaviour
{
    private float change = 0;
    private int Zone = 0;
    NavMeshAgent agent;
    public float baseSpeed = 3;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In ChestZones.cs");
        Debug.Log(other.tag);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (other.tag == "FastZone")
        {
            Debug.Log(other.tag);
            Zone = 1;
            change = 6f;
            speedChange(change, Zone);
        }
        else if (other.tag == "MediumZone")
        {
            Debug.Log(other.tag);
            change = 4f;
            Zone = 2;
            speedChange(change, Zone);
        }
        else if (other.tag == "SlowZone")
        {
            Debug.Log(other.tag);
            Zone = 3;
            change = 0.5f;
            speedChange(change, Zone);
        }
        else
        {
            Debug.Log("No Zone found");
        }
    }
    public void speedChange(float change, int Zone)
    {
        if (Zone == 1)
        {
            Debug.Log("FastZone adjusting speed");
            agent.speed = baseSpeed * change;
        }
        else if (Zone == 2)
        {
            Debug.Log("MediumZone adjusting speed");
            agent.speed = baseSpeed * change;
        }
        else if (Zone == 3)
        {
            Debug.Log("SlowZone adjusting speed");
            agent.speed = baseSpeed * change;
        }
        else 
        {
            Debug.Log("Zone is out of range");
        }
    }
}
