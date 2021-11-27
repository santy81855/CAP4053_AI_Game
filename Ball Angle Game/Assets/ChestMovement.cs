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
     
     void Start()
     {
         xPos = Random.Range(minX, maxX);
         zPos = Random.Range(minZ, maxZ);
         desiredPos = new Vector3(xPos, transform.position.y, zPos);
     }
 
     void Update()
     {
         timer += Time.deltaTime * timerSpeed;
         if (timer >= timeToMove)
         {
             transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
             if (Vector3.Distance(transform.position, desiredPos) <= 10f)
             {
                 xPos = Random.Range(minX, maxX);
                 zPos = Random.Range(minZ, maxZ);
                 desiredPos = new Vector3(xPos, transform.position.y, zPos);
                 timer = 0.0f;
             }
         }
     }
}
