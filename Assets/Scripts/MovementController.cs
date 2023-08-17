using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;
    public GameObject dashIcon;
    private float horizontal;
    public TextMeshPro cooldownText;

    [Header("Player Movement Variables")]
    [SerializeField] private float speed = 2f; // Movement speed
    [SerializeField] private float jumpingPower = 2f; // Initial jump power
    [SerializeField] private float doubleJumpingPower = 8f; // Double jump power

    [Header("Player Dash Variables")]
    [SerializeField] private float dashDistance = 5f; // Distance covered by dash
    [SerializeField] private float dashDuration = 0.2f; // Duration of the dash
    [SerializeField] private int cooldownTime = 5; // Cooldown time between dashes

    private int jumpCount = 0;
    private bool isFacingRight = true;
    private bool isJumping = false;
    private bool isDashing = false;
    private bool isCoolingDown = false;

    void Update()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            Flip();
        }
        animator.SetBool("IsJumping", isJumping);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (IsGrounded() && !isJumping)
        {
            OnLand();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                isJumping = true;
                jumpCount = 1; // Reset jump count when grounded
            }
            else if (jumpCount < 2) // Check if double jump is available
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpingPower);
                isJumping = true;
                jumpCount++;
                animator.SetBool("IsDoubleJump", true); // Set double jump animation flag
            }
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            isJumping = false;
            animator.SetBool("IsDoubleJump", false); // Reset double jump animation flag
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDashing && !isCoolingDown)
        {
            StartCoroutine(PerformDash());
            dashIcon.SetActive(false); // Wy³¹cz ikonê dasha
            StartCoroutine(DashCooldown()); // Uruchom korutynê odliczaj¹c¹ cooldown
        }
    }
    private IEnumerator PerformDash()
    {
        Debug.Log("PerformDash");
        isDashing = true;
        animator.SetBool("isDashing", isDashing);

        // Zatrzymaj postaæ w miejscu na krótki moment przed dashem
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f); // Czas zatrzymania przed dashem

        Vector2 dashDirection = new Vector2(isFacingRight ? 1 : -1, 0);

        float dashStartTime = Time.time;
        float dashEndTime = dashStartTime + dashDuration;

        // Ustaw przyspieszenie, aby uzyskaæ efekt dasha
        while (Time.time < dashEndTime)
        {
            rb.AddForce(dashDirection.normalized * (dashDistance / dashDuration), ForceMode2D.Force);
            yield return null;
        }

        isDashing = false;
        animator.SetBool("isDashing", isDashing);
    }

    private IEnumerator DashCooldown()
    {
        isCoolingDown = true;
        dashIcon.SetActive(true); // W³¹cz ikonê dasha
        yield return new WaitForSeconds(cooldownTime); // Zast¹p cooldownTime w³aœciwym czasem cooldownu
        isCoolingDown = false;
        dashIcon.SetActive(false); // Wy³¹cz ikonê dasha po zakoñczeniu cooldownu
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnLand()
    {
        isJumping = false;
        jumpCount = 0;
    }
}
