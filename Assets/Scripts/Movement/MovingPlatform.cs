using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    public float movementSpeed = 5f; // Set the movement speed of the platform
    public float movementDistance = 5f; // Set the distance the platform should cover

    private Vector3 initialPosition; // Initial position of the platform

    private void Start()
    {
        initialPosition = transform.position; // Remember the initial position
    }

    private void Update()
    {
        // Calculate the new position of the platform
        float offset = Mathf.PingPong(Time.time * movementSpeed, movementDistance);
        Vector3 newPosition = initialPosition + new Vector3(offset, 0f, 0f);

        // Move the platform to the new position
        transform.position = newPosition;
    }
}
