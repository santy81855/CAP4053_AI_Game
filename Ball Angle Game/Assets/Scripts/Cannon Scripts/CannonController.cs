using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    // Cannon firing variables
    Rigidbody cannonballRB;
    public GameObject cannonBall;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower;
    public float fireRate = 2f;
    public int powerMultiplier = 100;
    public float mouseSensitivity = 100.0f;
    public Slider slider;
    
   
    
    
    private AudioSource fireSound;
    private ReloadLoader reloadLoader;
    private float rotY;
    private float rotX;
    private float nextTimeToFire = 0f;
    public bool cannonLock;
    public bool xCameraBlock;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SaveSense", mouseSensitivity);

        // Instantiate objects
        fireSound = AudioManager.Instance.FireAudio;
        reloadLoader = GameManager.Instance.GetComponent<ReloadLoader>();

        // Lock cursor and set up math
        Cursor.lockState = CursorLockMode.Locked;
        firePower *= powerMultiplier;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
       
    }

    void Update()
    {

        if (cannonLock == false)
        {

            
            // Get input from mouse
            float mouseX = Input.GetAxis("Mouse X") * 20;
            float mouseY = -Input.GetAxis("Mouse Y") * 20;

            // Apply speed and sensitivity to camera look movement
            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            rotX += mouseY * mouseSensitivity * Time.deltaTime;

            // Up/Down
            rotX = Mathf.Clamp(rotX, -75, 35);

            // Left/Right (only for level 1 we restrict this movement)

            if (xCameraBlock == true)
                rotY = Mathf.Clamp(rotY, -115, 115);

            // Set starting angle of the cannon
            Quaternion localRotation = Quaternion.Euler(rotX, rotY + 90, 0.0f);
            transform.rotation = localRotation;

            // Reloader logic
            if (Pause_Menu.GameIsPaused == false && Input.GetMouseButton(0) && Time.time >= nextTimeToFire && GameManager.Instance.hasExploded == false)
            {
                // Set the next time to fire, call the reload slider, and fire the cannon.
                nextTimeToFire = Time.time + fireRate;
                reloadLoader.LoadReload(nextTimeToFire, fireRate);
                FireCannon();
            }
        }
    }

    public void FireCannon()
    {
        // Instantiate a ball and fire the ball. Play the sound as well.
        GameObject cannonBallCopy = Instantiate(cannonBall, shotPos.position, transform.rotation);
        cannonballRB = cannonBallCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, shotPos.position, shotPos.rotation);
        fireSound.Play();
    }

    public void SetSensitvity(float sense)
    {
        mouseSensitivity = sense;
        PlayerPrefs.SetFloat("SaveSense", mouseSensitivity);
        Debug.Log(sense);
    }
}