using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] float despawnTime = 3f;
    private GameManager gameManager;
    private bool hitEnemy = false;
    
    void Start()
    {
        // Reference the GameManager Singleton
        gameManager = GameManager.instance;
    }

    // Detects if there is a collision with an object and sends
    // data to the correct place.
    void OnCollisionEnter(Collision other)
    {
        // Start the despawn coroutine
        StartCoroutine(DespawnBall());

        // Get the enemy from the other object if there is one
        EnemyRagdoll enemy = other.transform.GetComponent<EnemyRagdoll>();

        // If it did hit an enemy, kill the enemy and mark good accuracy
        if (enemy != null)
        {
            enemy.die(despawnTime);
            hitEnemy = true;
        }
        else
        {
            // Nothing! Wait for the coroutine to destroy the ball.
        }
    }

    // Coroutine that despawns the ball after a certain amount of time.
    IEnumerator DespawnBall()
    {
        // Wait 5 seconds, then pass the hit to the GameManager and destroy the ball
        yield return new WaitForSecondsRealtime(5);
        gameManager.UpdateAccuracy(hitEnemy);
        Destroy(gameObject);
    }
}
