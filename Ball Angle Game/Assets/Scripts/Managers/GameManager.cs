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
        cannonBall.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PowerUp(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PowerUp(1);
        /*
            These PowerUps will be implemented at a different time
        if (Input.GetKeyDown("Alpha3"))
            PowerUp(2);
        if (Input.GetKeyDown("Alpha4"))
            PowerUp(3);
        */
    }
    public void CompleteLevel()
    {
        
        Debug.Log("LEVEL WON!");
        waveText.SetActive(false);
        accuracyText.SetActive(false);
        completeLevelUI.SetActive(true);
        winAudio.Play();

    }

    public void LostLevel()
    {
        Debug.Log("LEVEL LOST! TRY AGAIN!");
        lostLevelUI.SetActive(true);
        loseAudio.Play();
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
        Debug.Log("POWERUP: FAST FIRE!!!");
        float temp = cannon.fireRate;
        cannon.fireRate /= 2f;
        yield return new WaitForSecondsRealtime(7);
        cannon.fireRate = temp;
    }

    IEnumerator BigBall()
    {
        Debug.Log("POWERUP: BIG BALLS!!!");
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
