using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Transform target; // Reference to the enemy's Transform
    public Slider healthSlider;

    public EnemyHealth EnemyHealth;

    [Header("HP UI Variables")]
    public Gradient gradient;

    void UpdateHealthBarColor(Slider slider)
    {
        float normalizedHP = slider.value / slider.maxValue;
        Color healthColor = gradient.Evaluate(normalizedHP); // Get color from the Gradient

        // Set health bar color
        Image fillImage = slider.fillRect.GetComponent<Image>();
        fillImage.color = healthColor;
    }

    void Start()
    {
        UpdateHealthBarColor(healthSlider);
        // Find the enemy GameObject in the scene based on its tag or layer
        GameObject enemy = GameObject.FindGameObjectWithTag("EnemyTag"); // Replace "EnemyTag" with your enemy's tag or use layer-based detection
        if (enemy != null)
        {
            target = enemy.transform;
        }
        else
        {
            Debug.LogWarning("Enemy not found! Make sure to set the correct tag or layer.");
        }

        // Set the initial values of the slider (minValue, maxValue, currentValue)
        healthSlider.minValue = 0; // Minimum health value
        healthSlider.maxValue = EnemyHealth.maxHealth; // Maximum health value (adjust according to your game)
        healthSlider.value = healthSlider.maxValue; // Initial health value (set to maximum at the start)
    }

    void Update()
    {
        UpdateHealthBarColor(healthSlider);
        // Update the UI slider to display the enemy's remaining health
        if (target != null)
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Set the slider value to represent the enemy's current health
                healthSlider.value = enemyHealth.GetCurrentHealth();

                // Optional: You can also update the slider's fill color based on the enemy's health status (e.g., change color when health is low)
                // healthSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, healthSlider.normalizedValue);
            }
        }
        else
        {
            // Handle the case when the enemy is not found (optional)
            // Set the slider value to a default or unknown value
            healthSlider.value = 0; // Set to a default value (e.g., zero) or display unknown health
        }

        // Make the UI Slider face the camera (optional)
        transform.forward = Camera.main.transform.forward;
    }

}
