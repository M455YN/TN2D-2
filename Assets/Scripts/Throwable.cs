using UnityEngine;

public class Throwable : MonoBehaviour
{
    public int shurikenDamage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the shuriken collides with an enemy
        if (other.CompareTag("EnemyTag")) // Replace "EnemyTag" with your enemy's tag
        {
            // Get the EnemyHealth component from the collided enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Check if the enemy has the EnemyHealth component
            if (enemyHealth != null)
            {
                // Deal damage to the enemy
                enemyHealth.TakeDamage(shurikenDamage);
            }

            // Destroy the shuriken after hitting the enemy
            Destroy(gameObject);
        }
    }
}
