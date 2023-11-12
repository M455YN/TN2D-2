using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform component
    public float minYPosition = -10f; // Set the minimum Y position for triggering game over

    void Update()
    {
        // Check if the player's Y position is below the minimum allowed position
        if (player.position.y < minYPosition)
        {
            // Call the game over function or display game over screen here
            GameOver();
        }
    }

    void GameOver()
    {
        // Add your game over logic here, for example:
        // You can load a game over scene, display a game over UI, or restart the level.
        // Example: SceneManager.LoadScene("GameOverScene");
        // Example: gameOverUI.SetActive(true);
        // Example: RestartLevel();

        // For now, let's just log a message to the console.

        SceneManager.LoadScene("GameOverScene");
        Debug.Log("Game Over! Player fell out of the map.");
    }
}
