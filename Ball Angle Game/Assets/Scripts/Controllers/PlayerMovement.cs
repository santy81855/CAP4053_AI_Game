using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0f, 0f, zPress());

        controller.Move(move * speed * Time.deltaTime);

        // This would be the code for full wasd movement.
        // But we only get z axis for this project for left and right.
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        //Vector3 move = transform.right * x + transform.forward * z;

        //controller.Move(move * speed * Time.deltaTime);
    }

    public float zPress()
    {
        if (Input.GetKey("a"))
            return 0.5f;
        else if (Input.GetKey("d"))
            return -0.5f;
        else
            return 0f;
    }

}
