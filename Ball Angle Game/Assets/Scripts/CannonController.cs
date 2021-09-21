using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Cannon Rotation Variables
    public int speed;
    public float friction;
    public float lerpSpeed;
    float xDegrees;
    float yDegrees;
    Quaternion fromRotation;
    Quaternion toRotation;
    private new Camera camera;

    // Cannon Firing Variables
    public GameObject cannonBall;
    Rigidbody cannonballRB;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public int powerMultiplier = 100;
    // Start is called before the first frame update
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
    

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis
    void Start()
    {
        camera = Camera.main;
        firePower *= powerMultiplier;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {

        //Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float mouseX = Input.GetAxis("Mouse X") * 20;
        float mouseY = -Input.GetAxis("Mouse Y") * 20;

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);


        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

        //xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
        //yDegrees += Input.GetAxis("Mouse X") * speed * friction;
        //rotationX = ClampAngle(xDegrees, minimumX, maximumX);
        //fromRotation = transform.rotation;

        // Initial position of the cannon


        //transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);

        //rotationY = ClampAngle(yDegrees, minimumY, maximumY);
        //toRotation = Quaternion.Euler(rotationX, 90 + rotationY, 0);
        //Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        //Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);


        if (Input.GetMouseButtonDown(0))
        {
            FireCannon();
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    public void FireCannon()
    {
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, transform.rotation) as GameObject;
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);
    }
}
