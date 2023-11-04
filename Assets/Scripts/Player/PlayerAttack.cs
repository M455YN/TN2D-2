using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private MovementController MovementController;
    public Animator animator;

    [Header("Distance Attack (Shuriken)")]
    public int shurikenCount;
    public GameObject throwable;
    public float throwForce = 10f;
    public int shurikenDamage = 10;

    [Header("Melee Attack (Normal)")]
    public int meleeDamage = 20;
    public Transform attackCheck;
    public float attackRange = 1.5f;

    private bool meleeAttackReady = true;
    public void OnShurikenAttack(InputAction.CallbackContext context)
    {
        if (context.performed && shurikenCount > 0)
        {
            Debug.Log("Shuriken attack performed!"); // Debug log for testing

            Vector2 throwDirection = MovementController.isFacingRight ? Vector2.right : Vector2.left;
            GameObject thrownShuriken = Instantiate(throwable, transform.position, Quaternion.identity);
            Rigidbody2D rb = thrownShuriken.GetComponent<Rigidbody2D>();

            // Set shuriken layer to ignore collisions with the player
            thrownShuriken.layer = LayerMask.NameToLayer("Shuriken");

            // Apply force to the shuriken based on the throw direction
            rb.velocity = throwDirection * throwForce;

            // Add damage component to the shuriken for collision detection
            ShurikenDamage shurikenDamageComponent = thrownShuriken.AddComponent<ShurikenDamage>();
            shurikenDamageComponent.SetDamage(shurikenDamage);

            shurikenCount--;
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !MovementController.isJumping && meleeAttackReady)
        {
            meleeAttackReady = false;
            Debug.Log("Melee attack performed!"); // Debug log for testing

            animator.SetBool("MeleeAttack", true);

            // Check for enemies in melee range using AttackCheck object
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCheck.position, attackRange, LayerMask.GetMask("Enemy"));

            foreach (Collider2D enemyCollider in hitEnemies)
            {
                // Ensure the AttackCheck object is touching the enemy
                if (enemyCollider.bounds.Contains(attackCheck.position))
                {
                    // Deal damage to the enemy
                    EnemyHealth enemyHealth = enemyCollider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(meleeDamage);
                    }
                }
            }
        }
    }

    public void EndOfMeleeAttack()
    {
        meleeAttackReady = true;
        animator.SetBool("MeleeAttack", false);
    }

    void Update()
    {
        // You can add any additional logic here if needed
    }
}
