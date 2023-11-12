using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Flash the enemy in red when taking damage
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;

            // Reset the color back to the original color after a short delay
            Invoke("ResetColor", 0.1f);
        }

        // Check if the enemy's health has dropped to or below zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        // Perform death-related actions here, such as playing death animation, dropping items, etc.
        Destroy(gameObject); // Destroy the enemy GameObject when it dies
    }

    void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
}
