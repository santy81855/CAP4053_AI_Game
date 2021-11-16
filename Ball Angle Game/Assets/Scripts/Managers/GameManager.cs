using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }
    public GameObject originalObject;
    public GameObject fracturedObject;
    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    #endregion
    public GameObject cannonBall;
    private AudioSource winAudio;
    private AudioSource loseAudio;
    public GameObject accuracyText;
    public GameObject waveText;
    public GameObject treasure;
    public CannonController cannon;
    public GameObject shopUI;
    public enum PowerState { REGULAR, BLAST, FREEZE };
    public PowerState state = PowerState.REGULAR;

    public TMP_Text abilityCount1;
    public TMP_Text abilityCount2;
    public TMP_Text abilityCount3;
    public TMP_Text abilityCount4;

    public GameObject completeLevelUI;
    public GameObject lostLevelUI;

    private float ballCount = 0;
    private float ballHit = 0;
    private bool enableLock1 = false;
    private bool enableLock2 = false;
    private bool enableLock3 = false;
    private bool enableLock4 = false;

    void Start()
    {
        winAudio = AudioManager.Instance.WinAudio;
        loseAudio = AudioManager.Instance.LoseAudio;
        cannonBall.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && (int.Parse(abilityCount1.text) != 0) && enableLock1 == false)
        {
            gameObject.GetComponent<ShopManager>().ConsumeCharge(1);
            PowerUp(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && (int.Parse(abilityCount2.text) != 0) && enableLock2 == false)
        {
            gameObject.GetComponent<ShopManager>().ConsumeCharge(2);

            PowerUp(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && (int.Parse(abilityCount3.text) != 0) && enableLock3 == false)
        {
            gameObject.GetComponent<ShopManager>().ConsumeCharge(3);
            PowerUp(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && (int.Parse(abilityCount4.text) != 0) && enableLock4 == false)
        {
            gameObject.GetComponent<ShopManager>().ConsumeCharge(4);
            PowerUp(3);
        }

        if (Input.GetKeyDown(KeyCode.B) && shopUI.activeSelf)
        {
            Debug.Log("Exiting Shop");
            shopUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            cannon.cannonLock = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !shopUI.activeSelf)
        {
            Debug.Log("Entering Shop");
            shopUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cannon.cannonLock = true;
        }

    }
    public void CompleteLevel()
    {

        Debug.Log("LEVEL WON!");
        waveText.SetActive(false);
        accuracyText.SetActive(false);
        completeLevelUI.SetActive(true);
        winAudio.Play();

    }

    // function to explode the shatter_cannon prefab
    public void Explode()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy(t.gameObject, destroyDelay);
        }
    }

    // function that needs to be called whenever the cannon needs to explode
    public void SpawnFracturedObject()
    {
        // spawn the fractured version of the cannon
        GameObject fractObj = Instantiate(fracturedObject, originalObject.transform.position, originalObject.transform.rotation) as GameObject;
        // explode the cannon

        Explode();
        // remove the original cannon
        Destroy(originalObject);
    }

    public void LostLevel()
    {
        // start a coroutine so that we can wait a couple of seconds before the level lost UI pops up
        StartCoroutine(waiter());
    }

    // coroutine to wait a few seconds between operations
    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(1);
        // when the person loses we want to shatter the cannon
        SpawnFracturedObject();
        loseAudio.Play();
        yield return new WaitForSecondsRealtime(1);
        Debug.Log("LEVEL LOST! TRY AGAIN!");
        lostLevelUI.SetActive(true);
    }

    public void PowerUp(int powerIndex)
    {
        // POWERUP INDEX
        //-------------------------------
        // 0 -> Fast Fire Rate
        // 1 -> Bigger Cannon Balls
        // 2 -> Explosive Cannon Balls
        // 3 -> AOE Freeze Cannon Balls
        if (powerIndex == 0)
            StartCoroutine(FastFireRate());
        else if (powerIndex == 1)
            StartCoroutine(BigBall());
        else if (powerIndex == 2)
            StartCoroutine(BlastBall());
        else if (powerIndex == 3)
            StartCoroutine(FreezeBall());
    }

    IEnumerator FastFireRate()
    {
        enableLock1 = true;
        Debug.Log("POWERUP: FAST FIRE!!!");
        float temp = cannon.fireRate;
        cannon.fireRate /= 2f;
        yield return new WaitForSecondsRealtime(7);
        cannon.fireRate = temp;
        enableLock1 = false;
    }

    IEnumerator BigBall()
    {
        enableLock2 = true;
        Debug.Log("POWERUP: BIG BALLS!!!");
        cannonBall.transform.localScale = new Vector3(2f, 2f, 2f);
        yield return new WaitForSecondsRealtime(12);
        cannonBall.transform.localScale = new Vector3(1f, 1f, 1f);
        enableLock2 = false;
    }

    IEnumerator BlastBall()
    {
        enableLock3 = true;
        Debug.Log("POWERUP: BLAST BALL!!!");
        state = PowerState.BLAST;
        yield return new WaitForSecondsRealtime(7);
        state = PowerState.REGULAR;
        enableLock3 = false;
    }

    IEnumerator FreezeBall()
    {
        enableLock4 = true;
        Debug.Log("POWERUP: FREEZE BALL!!!");
        state = PowerState.FREEZE;
        yield return new WaitForSecondsRealtime(7);
        state = PowerState.REGULAR;
        enableLock4 = false;
    }

    public void UpdateAccuracy(bool hit)
    {
        if (hit)
            accuracyText.GetComponent<TMP_Text>().text = "Accuracy: " + (int)((++ballHit / ++ballCount) * 100) + "%";
        else
            accuracyText.GetComponent<TMP_Text>().text = "Accuracy: " + (int)((ballHit / ++ballCount) * 100) + "%";
    }
}
