using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannonBall : MonoBehaviour
{
    // Initialize variables
    [SerializeField] float despawnTime = 3f;
    private float radius = 100f;
    private ShopManager shopManager;
    private GameManager gameManager;
    private bool hitEnemy = false;

    void Start()
    {
        // Reference the GameManager Singleton.
        gameManager = GameManager.Instance;
        shopManager = ShopManager.Instance;

    }


    // Detects if there is a collision with an object and sends
    // data to the correct place.
    void OnCollisionEnter(Collision other)
    {

        if (gameManager.state == GameManager.PowerState.REGULAR)
        {
            // Start the despawn coroutine
            StartCoroutine(DespawnBall());

            // Get the enemy from the other object if there is one
            EnemyRagdoll enemy = other.transform.GetComponent<EnemyRagdoll>();

            // If it did hit an enemy, kill the enemy and mark good accuracy.
            if (enemy != null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                foreach (Collider nearbyObject in colliders)
                {
                    EnemyRagdoll enemyInRange = nearbyObject.transform.GetComponent<EnemyRagdoll>();
                    if (enemyInRange != null)
                    {
                        float distance = Vector3.Distance(enemyInRange.GetComponent<Transform>().position, transform.position);

                        Vector3 dirToBall = enemyInRange.GetComponent<Transform>().position - transform.position;
                        Vector3 newPos = enemyInRange.GetComponent<Transform>().position + dirToBall;

                        int dice = Random.Range(1, 4);
                        // if (dice == 1)
                        // {
                        //     newVec = new Vector3(0.0f, 0.0f, 20.0f);
                        // }
                        // // If two, set the enemy destination 20f right of itself.
                        // else if (dice == 2)
                        // {
                        //     newVec = new Vector3(0.0f, 0.0f, -20.0f);
                        // }
                        // else
                        // {
                        //     newVec = new Vector3(20.0f, 0.0f, 0.0f);
                        // }

                        dice = Random.Range(1, 5);
                        if (dice == 1)
                            StartCoroutine(EnemyFlee(newPos, enemyInRange));
                    }
                    else
                    {

                    }
                }
                if (enemy.GetComponent<EnemyStats>().hp == 1)
                {
                    enemy.GetComponent<StateManager>().isEnemyDead = true;
                    enemy.GetComponent<StateManager>().enabled = false;
                    enemy.die(despawnTime);
                    hitEnemy = true;
                    shopManager.addCoins();
                }
                else
                {
                    enemy.GetComponent<EnemyStats>().hp--;
                }

            }
            else
            {
                // Nothing! Wait for the coroutine to destroy the ball.
            }
        }
        else if (gameManager.state == GameManager.PowerState.BLAST)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                EnemyRagdoll enemy = nearbyObject.transform.GetComponent<EnemyRagdoll>();
                if (enemy != null)
                {
                    // if (enemy.enemyHealth == 0)
                    enemy.GetComponent<StateManager>().isEnemyDead = true;
                    enemy.GetComponent<StateManager>().enabled = false;
                    enemy.GetComponent<Animation>().enabled = false;
                    enemy.die(despawnTime);
                    hitEnemy = true;
                    shopManager.addCoins();
                    gameManager.UpdateAccuracy(hitEnemy);
                    Destroy(gameObject);
                }
                else
                {

                }
            }
        }
        else if (gameManager.state == GameManager.PowerState.FREEZE)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                EnemyRagdoll enemy = nearbyObject.transform.GetComponent<EnemyRagdoll>();
                if (enemy != null)
                {
                    NavMeshAgent agent = nearbyObject.transform.GetComponent<NavMeshAgent>();
                    StartCoroutine(FreezeTime(agent));
                    hitEnemy = true;
                    shopManager.addCoins();
                    gameManager.UpdateAccuracy(hitEnemy);
                }
            }
        }
    }

    // Coroutine that despawns the ball after a certain amount of time.
    IEnumerator DespawnBall()
    {
        // Wait 5 seconds, then pass the hit to the GameManager and destroy the ball.
        yield return new WaitForSecondsRealtime(5);
        gameManager.UpdateAccuracy(hitEnemy);
        Destroy(gameObject);
    }
    IEnumerator FreezeTime(NavMeshAgent agent)
    {
        agent.isStopped = true;
        yield return new WaitForSecondsRealtime(5);
        agent.isStopped = false;
        Destroy(gameObject);
    }

    IEnumerator EnemyFlee(Vector3 myvec, EnemyRagdoll enemyInRange)
    {
        enemyInRange.GetComponent<NavMeshAgent>().SetDestination(myvec);
        enemyInRange.GetComponent<StateManager>().enabled = false;
        yield return new WaitForSecondsRealtime(3);
        if (enemyInRange.GetComponent<StateManager>().isEnemyDead == false)
            enemyInRange.GetComponent<StateManager>().enabled = true;
    }
}