using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject rozpadanie;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Throwable") && !other.gameObject.CompareTag("Ladder"))
        {
            Destroy(this.gameObject);
            Instantiate(rozpadanie, transform.position, Quaternion.identity);
        }
    }
}
