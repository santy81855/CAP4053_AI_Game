using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMovement : MonoBehaviour
{
     public float speed = 0.05f;
     public float xPos;
     public float zPos;
     public Vector3 desiredPos;
     public float timer = 1f;
     public float timerSpeed = 0f; 
     public float timeToMove = 0.5f;
     public float minZ = -50.0f;
     public float maxZ = 9.0f;
     public float minX = -10.0f;
     public float maxX = 100.0f;
     // get the spawners
     public GameObject spawner;
     
     void Start()
     {
         xPos = Random.Range(minX, maxX);
         zPos = Random.Range(minZ, maxZ);
         desiredPos = new Vector3(xPos, transform.position.y, zPos);
     }

    void Update()
    {
        /* get the accuracy */
        GameObject manager = GameObject.Find("GameManager");
        GameManager acc = manager.GetComponent<GameManager>();
        float accuracy;
        // default to 50% if it is 0
        if (acc.ballHit == 0 || acc.ballCount == 0)
            accuracy = 0.5f;
        else
            accuracy = acc.ballHit / acc.ballCount;

        timer += Time.deltaTime * timerSpeed;
        if (timer >= timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 10f)
            {
                // get random number
                int number = Random.Range(1, 100);
                if (accuracy >= 0.5f)
                {
                    if (number < 25)
                        desiredPos = GameObject.Find("Spawn1").transform.position;
                    else if (number < 50)
                        desiredPos = GameObject.Find("Spawn2").transform.position;
                    else if (number < 75)
                        desiredPos = GameObject.Find("Spawn3").transform.position;
                    else
                        desiredPos = GameObject.Find("Spawn4").transform.position;
                    timer = 0.0f;
                }
                else
                {
                    xPos = Random.Range(minX, maxX);
                    zPos = Random.Range(minZ, maxZ);
                    desiredPos = new Vector3(xPos, transform.position.y, zPos);
                    timer = 0.0f;
                }
            }
        }
    }
}
