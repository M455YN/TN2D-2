using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movementSpeed = 5f; // Set the movement speed of the platform
    public float movementDistance = 5f; // Set the distance the platform should cover

    private Vector3 initialPosition; // Initial position of the platform
    private GameObject player;
    private GameObject playerClone;
    private bool onPlatform = false;

    private void Start()
    {
        initialPosition = transform.position; // Remember the initial position
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            onPlatform = true;

            // Create a clone of the player and make it a child of the platform
            playerClone = Instantiate(player, transform.position, transform.rotation);
            playerClone.transform.parent = transform;
            player.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            onPlatform = false;

            // Set the original player active and destroy the clone
            player.SetActive(true);
            Destroy(playerClone);
        }
    }

    private void Update()
    {
        // Calculate the new position of the platform
        float offset = Mathf.PingPong(Time.time * movementSpeed, movementDistance);
        Vector3 newPosition = initialPosition + new Vector3(offset, 0f, 0f);

        // Move the platform to the new position
        transform.position = newPosition;

        // If the player is on the platform, move the player clone with the platform
        if (onPlatform)
        {
            playerClone.transform.position = transform.position;
        }
    }
}
