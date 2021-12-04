using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CannonBall : MonoBehaviour
{
    // Initialize variables
    [SerializeField] float despawnTime = 3f;
    private float radius = 5f;
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


                // Update accuracy boolean
                hitEnemy = true;

                // Instantiate colliders array with all objects around the ball
                Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

                // Loop through each object
                foreach (Collider nearbyObject in colliders)
                {
                    // Grab the EnemyRagdoll component
                    EnemyRagdoll enemyInRange = nearbyObject.transform.GetComponent<EnemyRagdoll>();

                    // If theres an enemy, give it a chance to flee
                    if (enemyInRange != null)
                    {
                        int dice = Random.Range(1, 5);
                        if (dice == 1)
                        {
                            float distance = Vector3.Distance(enemyInRange.GetComponent<Transform>().position, transform.position);
                            Vector3 dirToBall = (enemyInRange.GetComponent<Transform>().position - transform.position) * 4f;
                            Vector3 newPos = (enemyInRange.GetComponent<Transform>().position + dirToBall);
                            enemyInRange.GetComponent<Flee>().GetFlee(newPos);
                        }
                    }
                }

                if (enemy.GetComponent<EnemyStats>().hp == 1)
                {
                    enemy.GetComponent<StateManager>().isEnemyDead = true;
                    enemy.GetComponent<StateManager>().enabled = false;
                    enemy.die(despawnTime);
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
                EnemyRagdoll enemyInRange = nearbyObject.transform.GetComponent<EnemyRagdoll>();
                if (enemyInRange != null)
                {
                    enemyInRange.GetComponent<StateManager>().isEnemyDead = true;
                    enemyInRange.GetComponent<StateManager>().enabled = false;
                    enemyInRange.die(despawnTime);
                    hitEnemy = true;
                    gameManager.UpdateAccuracy(hitEnemy);
                    shopManager.addCoins();
                    Destroy(gameObject);
                }
                else
                {

                }
            }
        }
        else if (gameManager.state == GameManager.PowerState.FREEZE)
        {
            EnemyRagdoll enemy = other.transform.GetComponent<EnemyRagdoll>();
            if (enemy != null)
            {
                enemy.GetComponent<StateManager>().isEnemyDead = true;
                enemy.die(despawnTime);
                hitEnemy = true;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                EnemyRagdoll enemyInRange = nearbyObject.transform.GetComponent<EnemyRagdoll>();
                if (enemyInRange != null)
                {
                    NavMeshAgent agent = nearbyObject.transform.GetComponent<NavMeshAgent>();
                    StartCoroutine(FreezeTime(agent));
                    hitEnemy = true;
                    shopManager.addCoins();
                }
            }

            gameManager.UpdateAccuracy(hitEnemy);
        }
    }

    // Coroutine that despawns the ball after a certain amount of time.
    IEnumerator DespawnBall()
    {
        // Wait 5 seconds, then pass the hit to the GameManager and destroy the ball.
        yield return new WaitForSeconds(5);
        gameManager.UpdateAccuracy(hitEnemy);
        Destroy(gameObject);
    }
    IEnumerator FreezeTime(NavMeshAgent agent)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(5);
        if (agent != null && agent.GetComponent<StateManager>().isEnemyDead == false)
            agent.isStopped = false;
        Destroy(gameObject);
    }

    // IEnumerator EnemyFlee(Vector3 myvec, EnemyRagdoll enemyInRange)
    // {
    //     enemyInRange.GetComponent<NavMeshAgent>().SetDestination(myvec);
    //     enemyInRange.GetComponent<StateManager>().enabled = false;
    //     yield return new WaitForSeconds(2);
    //     if (enemyInRange.GetComponent<StateManager>().isEnemyDead == false)
    //         enemyInRange.GetComponent<StateManager>().enabled = true;
    // }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}