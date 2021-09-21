using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;

    //public float speed = 6f;

    public float turnSmoothTime = 0.1f;

    public Vector2 turn;
    public float sensitivity = .5f;

    //float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Mouse X");
        //float vertical = Input.GetAxisRaw("Mouse Y");
        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        turn.x += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        turn.y += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(-turn.y, turn.x, 0); 
        /*
        if (direction.magnitude >= 0.1f)
        {
            //float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);    
               
            

            //Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }*/
    }

}
