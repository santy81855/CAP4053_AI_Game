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
    public enum SpawnState {  SPAWNING, WAITING, COUNTING };
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public PlayerManager playerManager;
    // allows us to change the values inside of unity
     
    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
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

            nextWave = 0;
            Debug.Log("All waves complete! Looping...");
            playerManager.CompleteLevel();
            
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
        Instantiate(_enemy, spawn1.transform.position, spawn1.transform.rotation);
        // spawn enemy
        //        Debug.Log("Spawning Enemy: " + _enemy.name);
        // Create 3 objects
        // Get random number
        // either 1 2 or 3
        // if random == number
        // spawn at gameobject -> Instantiate()
        // Generate a random number and spawn at one of the locations.
        /*int spawnNumber = Random.Range(1, 4);
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
        else
        {
            //Debug.Log(spawnNumber);
            Instantiate(_enemy, spawn3.transform.position, spawn3.transform.rotation);
        }*/
    }

    void OnTriggerEnter()
    {
        
    }

    IEnumerator WaveNumberText()
    {
        waveText.GetComponent<TMP_Text>().text = "Wave: " + (nextWave + 1);
        waveText.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        waveText.SetActive(false);
    }
}
