using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    public GameObject waveText;
    [System.Serializable]

    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public enum SpawnState {  SPAWNING, WAITING, COUNTING, ENDING };
    public GameObject bigBall;
    public GameObject fastFire;
    public Transform bigBallSpawn;
    public Transform fastFireSpawn;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public PlayerManager playerManager;
    // allows us to change the values inside of unity
     
    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    private int ffFlag = 0;
    private int bbFlag = 0;
    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
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
            // Check to see if enemies are still alive in the wave
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        // Check if the countdown of the wave has hit zero
        if (waveCountdown <= 0)
        {
            if (nextWave == 1 && ffFlag == 0)
            {
                Instantiate(fastFire, fastFireSpawn.position, fastFireSpawn.rotation);
                ffFlag = 1;
            }
            if (nextWave == 2 && bbFlag == 0)
            {
                Instantiate(bigBall, bigBallSpawn.position, bigBallSpawn.rotation);
                bbFlag = 1;
            }
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
            StartCoroutine(WaitForWin());
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
        return false;
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            
            // When the timer is up, check if there are any enemies left.
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        // Loop through and spawn enemies
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        int spawnNumber = Random.Range(1, 6);
        if (spawnNumber == 1)
        {
            //Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn1.transform.position, spawn1.transform.rotation);

        }
        else if (spawnNumber == 2)
        {
            //Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn2.transform.position, spawn2.transform.rotation);
        }
        else if (spawnNumber == 3)
        {
            //Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn3.transform.position, spawn3.transform.rotation);
        }
        else if (spawnNumber == 4)
        {
            //Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn4.transform.position, spawn4.transform.rotation);
        }
        else if (spawnNumber == 5)
        {
            //Debug.Log(spawnNumber);
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

    IEnumerator WaitForWin()
    {
        yield return new WaitForSecondsRealtime(15);
        playerManager.CompleteLevel();
    }
}
