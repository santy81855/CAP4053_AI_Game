using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Cannon Rotation Variables
    public int speed;
    public float friction;
    public float lerpSpeed;
    public float firePower;
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
    // Start is called before the first frame update
    //
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Cannon")
            {
                if (Input.GetMouseButton(0))
                {
                    xDegrees -= Input.GetAxis("Mouse Y") * speed * friction;
                    yDegrees += Input.GetAxis("Mouse X") * speed * friction;
                    fromRotation = transform.rotation;
                    toRotation = Quaternion.Euler(0, yDegrees, xDegrees);
                    transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            FireCannon();
        }
    }

    public void FireCannon()
    {
        firePower *= 2000;
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, transform.rotation) as GameObject;
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);

    }
}
