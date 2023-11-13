using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f, jumpForce = 100f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true, isJumping, isInAir;
    public bool directionLookEnabled = true;

    [Header("Enemy Shuriken Attack")]
    public GameObject shurikenPrefab;
    public Transform shurikenSpawnPoint;
    public float shurikenCooldown = 2f;
    private float shurikenTimer = 0f;
    public int shurikenCount;
    public GameObject throwable;
    public float throwForce = 10f;

    [SerializeField] Vector3 startOffset;
    SpriteRenderer spriteRenderer;

    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] public RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private bool isOnCoolDown;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isJumping = false;
        isInAir = false;
        isOnCoolDown = false;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    void Update()
    {
        shurikenTimer += Time.deltaTime;

        //if (TargetInDistance() && followEnabled && shurikenTimer >= shurikenCooldown)
        //{
        //    ShootShuriken();
        //}
    }

    void ShootShuriken()
    {
        shurikenTimer = 0f;

        GameObject shuriken = Instantiate(shurikenPrefab, shurikenSpawnPoint.position, Quaternion.identity);
        Rigidbody2D shurikenRb = shuriken.GetComponent<Rigidbody2D>();

        Vector2 directionToPlayer = (target.position - shurikenSpawnPoint.position).normalized;
        shurikenRb.velocity = directionToPlayer * throwForce;

        shuriken.layer = LayerMask.NameToLayer("Shuriken");

        ShurikenDamageAI shurikenDamageComponent = shuriken.AddComponent<ShurikenDamageAI>();
        shurikenDamageComponent.SetDamage(0);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled && Vector2.Distance(transform.position, target.position) > 5f)
        {
            PathFollow();
        }

        if (Vector2.Distance(transform.position, target.position) <= 5f && shurikenTimer >= shurikenCooldown)
        {
            ShootShuriken();
            Debug.Log("Przeciwnik jest w odleg³oœci 2 jednostek od celu!");
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset, transform.position.z);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        if (jumpEnabled && isGrounded && !isInAir && !isOnCoolDown)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                if (isInAir) return;
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                StartCoroutine(JumpCoolDown());
            }
        }

        if (isGrounded)
        {
            isJumping = false;
            isInAir = false;
        }
        else
        {
            isInAir = true;
        }

        rb.velocity = new Vector2(force.x, rb.velocity.y);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                spriteRenderer.flipX = true;
            }
            else if (rb.velocity.x < -0.05f)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator JumpCoolDown()
    {
        isOnCoolDown = true;
        yield return new WaitForSeconds(1f);
        isOnCoolDown = false;
    }
}
