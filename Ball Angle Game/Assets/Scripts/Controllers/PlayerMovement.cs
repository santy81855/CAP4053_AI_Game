using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Vector3 circleCenter = new Vector3(-6.7f, 2.66f, -20.69f);
    public float radius = 30.0f;
    public float speed = 12f;

    // Update is called once per frame
    void Update()
    {
        // get input for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        // get the direction being faced by the camera
        float cameraFacing = Camera.main.transform.eulerAngles.y;
        // create the original vector holding the direction to move in
        Vector3 inputVector = new Vector3(x, 0, z);

        // rotate the original vector relative to the direction faced by the camera
        Vector3 turnedInputVector = Quaternion.Euler(0, cameraFacing, 0) * inputVector;
        // move the cannon if it is within the boundaries of the circle
        // get the difference from center and current vectors
        float minx = circleCenter.x - radius;
        float maxx = circleCenter.x + radius;
        float minz = circleCenter.z - radius;
        float maxz = circleCenter.z + radius;
        if (transform.position.x + turnedInputVector.x > minx && transform.position.x + turnedInputVector.x < maxx && transform.position.z + turnedInputVector.z > minz && transform.position.z + turnedInputVector.z < maxz)
            controller.Move(turnedInputVector * speed * Time.deltaTime);
    }
    // get the buttons for movement
    public float zPress()
    {
        if (Input.GetKey("a"))
            return 0.5f;
        else if (Input.GetKey("d"))
            return -0.5f;
        else
            return 0f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCenter, radius);
    }

}
