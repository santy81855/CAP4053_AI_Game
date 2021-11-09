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
    public GameObject player;
    public CannonController cannon;


    public GameObject completeLevelUI;
    public GameObject lostLevelUI;

    private float ballCount = 0;
    private float ballHit = 0;

    void Start()
    {
        winAudio = AudioManager.Instance.WinAudio;
        loseAudio = AudioManager.Instance.LoseAudio;
    }
    public void CompleteLevel()
    {
        
        Debug.Log("LEVEL WON!");
        waveText.SetActive(false);
        accuracyText.SetActive(false);
        completeLevelUI.SetActive(true);
        winAudio.Play();

    }

    public void Explode()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null) 
            {
                rb.AddExplosionForce (Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy (t.gameObject, destroyDelay);
        }
    }

    // function that needs to be called whenever the cannon needs to explode
    public void SpawnFracturedObject()
    {
        
        GameObject fractObj = Instantiate (fracturedObject, originalObject.transform.position, originalObject.transform.rotation) as GameObject;
        Explode();
        Destroy(originalObject);
    }

    public void LostLevel()
    {
        // start a coroutine so that we can wait a couple of seconds before the level lost UI
        // pops up
        StartCoroutine(waiter());
    }

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
        if (powerIndex == 0)
            StartCoroutine(FastFireRate());
        else if (powerIndex == 1)
            StartCoroutine(BigBall());
    }

    IEnumerator FastFireRate()
    {
        float temp = cannon.fireRate;
        cannon.fireRate /= 2f;
        yield return new WaitForSecondsRealtime(7);
        cannon.fireRate = temp;
    }

    IEnumerator BigBall()
    {
        cannonBall.transform.localScale = new Vector3(2f, 2f, 2f);
        yield return new WaitForSecondsRealtime(12);
        cannonBall.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void UpdateAccuracy(bool hit)
    {
        if (hit)
            accuracyText.GetComponent<TMP_Text>().text = "Accuracy: " + (int)((++ballHit / ++ballCount) * 100) + "%";
        else
            accuracyText.GetComponent<TMP_Text>().text = "Accuracy: " + (int)((ballHit / ++ballCount) * 100) + "%";
    }
}
