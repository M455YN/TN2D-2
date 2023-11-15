using UnityEngine;

public class ShurikenDamage : MonoBehaviour
{
    private int damage = 5;
    private PlayerAttack playerAttackScript;

    // Set the damage value for the shuriken
    public void SetDamage(int shurikenDamage)
    {
        damage = shurikenDamage;
    }

    // Set reference to the PlayerAttack script
    public void SetPlayerAttackScript(PlayerAttack attackScript)
    {
        playerAttackScript = attackScript;
    }

    // Handle collision with enemies
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyTag")) // Replace "EnemyTag" with your enemy's tag
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Check if the enemy has the EnemyHealth component
            if (enemyHealth != null)
            {
                // Deal damage to the enemy
                enemyHealth.TakeDamage(damage);
            }

            // Destroy the shuriken after hitting an enemy
            Destroy(gameObject);
        }
        
        //if (other.CompareTag("Player")) // Replace "EnemyTag" with your enemy's tag
        //{
        //    MovementController playerHealth = other.GetComponent<MovementController>();
        //
        //    // Check if the enemy has the EnemyHealth component
        //    if (playerHealth != null)
        //    {
        //        // Deal damage to the enemy
        //        playerHealth.TakeDamage(damage);
        //    }
        //
        //    // Destroy the shuriken after hitting an enemy
        //    Destroy(gameObject);
        //}

        // Check collision with the player to prevent friendly fire
        if (other.CompareTag("Player") && playerAttackScript != null)
        {
            // Ignore collision with the player, destroy the shuriken
            Destroy(gameObject);
        }
    }
}
