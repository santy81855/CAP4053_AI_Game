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
    public float minZ = -70.0f;
    public float maxZ = 40.0f;
    public float minX = -50.0f;
    public float maxX = 50.0f;
    // to store temp vectors
    private Vector3 temp;

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
        Debug.Log(accuracy);
        timer += Time.deltaTime;
        if (timer >= timeToMove)
        {
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, desiredPos) <= 10f || Mathf.Abs(transform.position.x - desiredPos.x) <= 10f || Mathf.Abs(transform.position.z - desiredPos.z) <= 10f)
            {
                // get random number
                int number = Random.Range(1, 125);
                if (accuracy >= 0.5f)
                {
                    if (number < 25)
                        temp = GameObject.Find("Spawn1").transform.position;
                    else if (number < 50)
                        temp = GameObject.Find("Spawn2").transform.position;
                    else if (number < 75)
                        temp = GameObject.Find("Spawn3").transform.position;
                    else if (number < 100)
                        temp = GameObject.Find("Spawn4").transform.position;
                    else
                        temp = GameObject.Find("Spawn5").transform.position;
                    // make the chest move towards the spawner but not quite to the spawner
                    if (temp.x > 0 && temp.z > 0)
                        desiredPos = new Vector3(temp.x - 40, transform.position.y, temp.z - 40);
                    else if (temp.x > 0 && temp.z < 0)
                        desiredPos = new Vector3(temp.x - 40, transform.position.y, temp.z + 40);
                    else if (temp.x < 0 && temp.z > 0)
                        desiredPos = new Vector3(temp.x + 40, transform.position.y, temp.z - 40);
                    else
                        desiredPos = new Vector3(temp.x + 40, transform.position.y, temp.z + 40);
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
