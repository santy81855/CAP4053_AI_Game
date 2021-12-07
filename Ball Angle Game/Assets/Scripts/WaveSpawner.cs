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
    public GameObject waveTracker;
    public GameObject shopReminderText;
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
    public SpawnState state = SpawnState.COUNTING;
    // fast fire powerfup flag
    private int ffFlag = 0;
    // big ball powerup flag
    private int bbFlag = 0;

    public LevelArray levelArray;

    public int numLevel;

    private int arrayCount = 0;
    private string[] spawnArray;
    public bool isIntermission = false;

    public Transform regularEnemy;
    public Transform tankEnemy;
    public Transform speedEnemy;
    // The 3 types of enemies are "REGULAR", "SPEED", and "TANK"
    // need to create an array that will spawn the correct amount of enemies depending on the input
    // and it also needs to make it more difficult depending on the person's accuracy
    // The number of waves is: waves.Length
    // The number of enemies in wave 1: waves[0].count
    private string[] waveArrayOne = new string[] { "TANK", "TANK", "TANK", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "SPEED", "TANK", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "TANK", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "TANK", "TANK", "TANK", "REGULAR", "SPEED", "REGULAR", "REGULAR", "REGULAR", "SPEED", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "REGULAR", "SPEED" };

    public string[] waveArrayTwo;
    public string[] waveArrayThree;

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = 5.0f;
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
            shopReminderText.SetActive(false);
            // fast fire powerup in wave 1
            // if (nextWave == 1 && ffFlag == 0)
            // {
            //     Instantiate(fastFire, fastFireSpawn.position, fastFireSpawn.rotation);
            //     ffFlag = 1;
            // }
            // // big ball powerup in wave 0
            // if (nextWave == 0 && bbFlag == 0)
            // {
            //     Instantiate(bigBall, bigBallSpawn.position, bigBallSpawn.rotation);
            //     bbFlag = 1;
            // }
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
        Debug.Log("Wave Completed!");

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
            shopReminderText.SetActive(true);
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
                Debug.Log("All Enemies have been eliminated");
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        // Here is where we create the array of enemies based on the person's accuracy
        // get the accuracy as a fraction
        GameObject manager = GameObject.Find("GameManager");
        GameManager acc = manager.GetComponent<GameManager>();
        float accuracy;
        if (acc.ballHit == 0 || acc.ballCount == 0)
            accuracy = 0.5f;
        else
            accuracy = acc.ballHit / acc.ballCount;

        // now using the accuracy we determine if we should increase the count of enemies for the wave
        // We will assume that 50% accuracy means you are normal
        // so we will multiply the enemy count by 2 * accuracy, so if your accuracy is 50% then you
        // will just get the normal amount of enemies

        // if the current accuracy is 0 it means that the person is on wave 1, so we will default
        // to a 50% accuracy
        Debug.Log(accuracy);

        // get the adjusted enemy 
        float enemyCount = _wave.count * 2 * accuracy;
        Debug.Log("Spawning the number below");
        Debug.Log(enemyCount);

        // we will assume that we want a normal enemy to appear 82% of the time and the hard
        // enemies to each appear 9% of the time if you have an accuracy of 50%.
        float normalEnemyConstant = 0.6098f; // we get this by dividing 50/82
        // since we want to spawn less normal enemies and more harder enemies when the person is
        // doing well we can just multiply it by 2 * accuracy, since it will keep the number
        // constant for an accuracy of 50% and will make the spawnrate of normal enemies lower for
        // higher accuracies
        float adjustedEnemyConstant = normalEnemyConstant * 2.0f * accuracy;
        // now we get the rates for the 3 enemy types
        float normalRate = 0.5f / adjustedEnemyConstant;
        // rate for fast enemies
        float fastRate = (1.0f - normalRate) / 2.0f;
        float tankRate = fastRate;
        // example: the wave is meant to have 10 enemies
        // the person has an accuracy of 0.65, which is considered better than average
        // the number of enemies gets adjusted to 10 * 0.65 * 2 = 13
        // the rate of normal enemies will go from 70% to 0.5 / (0.714 * 2 * 0.65) = 54%
        // this means the rate of fast and tank enemies will go from 15% each to 1 - 0.54 / 2 = 23%
        // this ensures that the game scales in difficulty as the person gets better

        // set the state to show that we are currently spawning enemies
        state = SpawnState.SPAWNING;

        // Loop through and spawn enemies
        for (int i = 0; i < (int)enemyCount; i++)
        {
            // We will spawn the enemies depending on the calculated rates above
            int number = Random.Range(1, 100);
            // example: if the number is under 50
            if (number <= (normalRate * 100))
                SpawnEnemy(regularEnemy);
            // ex: if the number is between 50 and 75
            else if (number > (normalRate * 100) && (number <= (100 - (tankRate * 100))))
                SpawnEnemy(tankEnemy);
            // ex: if the number is between 75 and 100
            else if (number > (100 - (fastRate * 100)))
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
            // Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn1.transform.position, spawn1.transform.rotation);

        }
        else if (spawnNumber == 2)
        {
            // Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn2.transform.position, spawn2.transform.rotation);
        }
        else if (spawnNumber == 3)
        {
            // Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn3.transform.position, spawn3.transform.rotation);
        }
        else if (spawnNumber == 4)
        {
            // Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn4.transform.position, spawn4.transform.rotation);
        }
        else if (spawnNumber == 5)
        {
            // Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn5.transform.position, spawn5.transform.rotation);
        }
    }

    IEnumerator WaveNumberText()
    {
        waveText.GetComponent<TMP_Text>().text = "Wave: " + (nextWave + 1);
        waveTracker.GetComponent<TMP_Text>().text = "Wave " + (nextWave + 1) + "/5";
        waveText.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        waveText.SetActive(false);
    }
}
