using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horzontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z);

        controller.Move(move * speed * Time.deltaTime);
    }
}
