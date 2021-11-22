using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{

    [System.Serializable]

    // Predefined variables for each wave
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
    }
    // all variables that need to be imported in the unity editor
    public enum SpawnState { SPAWNING, WAITING, COUNTING, ENDING };
    public GameObject waveText;
    public GameObject bigBall;
    public GameObject fastFire;
    public Transform bigBallSpawn;
    public Transform fastFireSpawn;
    // spawn locations
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    private GameManager gameManager;
    // array of predefined number of waves 
    public Wave[] waves;
    private int nextWave = 0;
    // Time between waves defaults to 5 seconds, but it can be changed in editor
    public float timeBetweenWaves = 5f;
    // the amount of time for a wave to start.
    public float waveCountdown;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    // fast fire powerfup flag
    private int ffFlag = 0;
    // big ball powerup flag
    private int bbFlag = 0;

    public LevelArray levelArray;

    public int numLevel;

    private int arrayCount = 0;
    private string[] spawnArray;

    public Transform regularEnemy;
    public Transform tankEnemy;
    public Transform speedEnemy;
    private string[] waveArrayOne = new string[] { "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "SPEED", "TANK", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "TANK", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "TANK", "TANK", "TANK", "REGULAR", "SPEED", "REGULAR", "REGULAR", "REGULAR", "SPEED", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "SPEED" };

    public string[] waveArrayTwo;
    public string[] waveArrayThree;

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
        gameManager = gameObject.GetComponent<GameManager>();

        if (numLevel == 1)
            spawnArray = waveArrayOne;
        else if (numLevel == 2)
            spawnArray = levelArray.waveArrayTwo;
        else if (numLevel == 3)
            spawnArray = levelArray.waveArrayThree;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.ENDING)
        {
            return;
        }
        // If we are in the wait state
        if (state == SpawnState.WAITING)
        {
            //  if no enemies are alive, then the wave is completed
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            // if any enemies are alive
            else
            {
                return;
            }
        }

        // Check if the countdown of the wave has hit zero
        if (waveCountdown <= 0)
        {
            // fast fire powerup in wave 1
            if (nextWave == 1 && ffFlag == 0)
            {
                Instantiate(fastFire, fastFireSpawn.position, fastFireSpawn.rotation);
                ffFlag = 1;
            }
            // big ball powerup in wave 0
            if (nextWave == 0 && bbFlag == 0)
            {
                Instantiate(bigBall, bigBallSpawn.position, bigBallSpawn.rotation);
                bbFlag = 1;
            }
            // if countdown reaches 0 and we are not spawning a wave then we spawn a wave
            if (state != SpawnState.SPAWNING)
            {
                // start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }

            StartCoroutine(WaveNumberText());
        }
        else
        {
            // Update the countdown
            waveCountdown -= Time.deltaTime;
        }

    }
    void WaveCompleted()
    {
        Debug.Log("Wave completed!");

        // Change the state to the counting state
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        // If this was the last wave, call complete screen.
        // Otherwise, move on to the next wave
        if (nextWave + 1 > waves.Length - 1)
        {
            gameManager.CompleteLevel();
            nextWave = 0;
            Debug.Log("All waves complete! Looping...");
            state = SpawnState.ENDING;

        }
        else
        {
            nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            // When the timer is up, check if there are any enemies left.
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                Debug.Log("WE HAVE NO ENEMIES ON THE BOARD");
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        // set the state to show that we are currently spawning enemies
        state = SpawnState.SPAWNING;

        // Loop through and spawn enemies
        for (int i = 0; i < _wave.count; i++)
        {
            Debug.Log("Count is : " + i);
            if (spawnArray[arrayCount] == "REGULAR")
                SpawnEnemy(regularEnemy);
            else if (spawnArray[arrayCount] == "TANK")
                SpawnEnemy(tankEnemy);
            else if (spawnArray[arrayCount] == "SPEED")
                SpawnEnemy(speedEnemy);

            arrayCount++;
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        // set the state to waiting once the enemies have been spawned
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        // randomly determine which of the 5 locations to spawn each enemy
        int spawnNumber = Random.Range(1, 6);
        if (spawnNumber == 1)
        {
            Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn1.transform.position, spawn1.transform.rotation);

        }
        else if (spawnNumber == 2)
        {
            Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn2.transform.position, spawn2.transform.rotation);
        }
        else if (spawnNumber == 3)
        {
            Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn3.transform.position, spawn3.transform.rotation);
        }
        else if (spawnNumber == 4)
        {
            Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn4.transform.position, spawn4.transform.rotation);
        }
        else if (spawnNumber == 5)
        {
            Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn5.transform.position, spawn5.transform.rotation);
        }
    }

    IEnumerator WaveNumberText()
    {
        waveText.GetComponent<TMP_Text>().text = "Wave: " + (nextWave + 1);
        waveText.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        waveText.SetActive(false);
    }
}
