using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannonBall : MonoBehaviour
{
    // Initialize variables
    [SerializeField] float despawnTime = 3f;
    [SerializeField] float radius = 5f;
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
                    enemy.die(despawnTime);
                    hitEnemy = true;
                    shopManager.addCoins();
                    gameManager.UpdateAccuracy(hitEnemy);
                    Destroy(gameObject);
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
}
