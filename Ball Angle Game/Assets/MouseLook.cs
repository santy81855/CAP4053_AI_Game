using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        mouseX = Mathf.Clamp(mouseX, -90f, 90f);
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        //xRotation = mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //playerBody.Rotate(0, mouseX, -mouseY, Space.World);
        if (mouseX > -90f && mouseX < 90f)
            playerBody.Rotate(Vector3.up * mouseX);
        if (mouseY > -3f && mouseY < 90f)
            playerBody.Rotate(Vector3.right * -mouseY);                
    }
}
