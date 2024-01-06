using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Referencja do transformacji gracza
    public Vector3 offset;    // Offset kamery od gracza

    void Update()
    {
        if (target != null)
        {
            // Oblicz docelow¹ pozycjê kamery
            Vector3 targetPosition = target.position + offset;
            // Ustaw pozycjê kamery na docelow¹ pozycjê z p³ynnym ruchem
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
