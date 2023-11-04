using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField][Range(0.0f, 10f)] private float speed = 5;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    [SerializeField] private bool leftP;
    private Rigidbody2D enemy;
    private SpriteRenderer obroc;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component from the object this script is attached to
        enemy = GetComponent<Rigidbody2D>();

        // Get the SpriteRenderer component from the object this script is attached to
        obroc = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.position.x < leftPoint.position.x)
        {
            leftP = false;
        }
        if (enemy.position.x > rightPoint.position.x)
        {
            leftP = true;
        }
        if (leftP)
        {
            obroc.flipX = false;
            enemy.velocity = new Vector2(-speed, enemy.velocity.y);
        }
        else
        {
            obroc.flipX = true;
            enemy.velocity = new Vector2(speed, enemy.velocity.y);
        }
    }
}
