using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zones : MonoBehaviour
{
    private float zone1SpeedMultiplier = 4f;
    private float zone2SpeedMultiplier = 1.2f;
    private float zone3SpeedMultiplier = 1.4f;
    // 1 = zone 1, 2 = zone 2, 3 = zone 3
    int currentZone = 0;

    public float zone1XStart = 17.0f;
    public float zone1XEnd = -10.0f;
    public float zone1YStart = -10.0f;
    public float zone1YEnd = 10.0f;
    public float zone2XStart = 53.0f;
    public float zone2XEnd = 17.0f;
    public float zone2YStart = -10.0f;
    public float zone2YEnd = 10.0f;
    public float zone3XStart = 53.0f;
    public float zone3XEnd = 110.0f;
    public float zone3YStart = -10.0f;
    public float zone3YEnd = 10.0f;
    public UnityEngine.AI.NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        if (position.x > zone3XStart && position.x < zone3XEnd && position.y > zone3YStart && position.y < zone3YEnd &&currentZone != 1)
        {
            agent.speed *= zone1SpeedMultiplier;
            currentZone = 1;
        }
        else if (position.x > zone2XStart && position.x < zone2XEnd && position.y > zone2YStart && position.y < zone2YEnd &&currentZone != 2)
        {
            agent.speed *= zone2SpeedMultiplier;
            currentZone = 2;
        }
        else if (position.x > zone3XStart && position.x < zone3XEnd && position.y > zone3YStart && position.y < zone3YEnd &&currentZone != 3)
        {
            agent.speed *= zone3SpeedMultiplier;
            currentZone = 3;
        }
    }
}
