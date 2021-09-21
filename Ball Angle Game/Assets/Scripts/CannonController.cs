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
    Camera camera;

    // Cannon Firing Variables
    public GameObject cannonBall;
    Rigidbody cannonballRB;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public int powerMultiplier = 100;
    // Start is called before the first frame update
    //


    void Start()
    {
        camera = Camera.main;
        firePower *= powerMultiplier;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);


 


        xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
        yDegrees += Input.GetAxis("Mouse X") * speed * friction;
        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                    


        if (Input.GetMouseButtonDown(0))
        {
            FireCannon();
        }
    }

    public void FireCannon()
    {
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, transform.rotation) as GameObject;
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);
    }
}
