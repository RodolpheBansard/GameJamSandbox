using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f;
    public int extraJumps = 0;
    public float dashDistance = 15f;
    public float dashTime = 0.3f;
    [SerializeField] Transform groundCheck;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;

    int jumpCount = 0;
    bool isGrounded;
    float movementx;
    float jumpCoolDown;

    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeycode;

    private void Update()
    {
        movementx = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(doubleTapTime > Time.time && lastKeycode == KeyCode.Q)
            {
                StartCoroutine(Dash(-1));
            }
            else
            {
                doubleTapTime = Time.time + 0.3f;
            }
            lastKeycode = KeyCode.Q;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeycode == KeyCode.D)
            {
                StartCoroutine(Dash(1));
            }
            else
            {
                doubleTapTime = Time.time + 0.3f;
            }
            lastKeycode = KeyCode.D;
        }

        CheckGrounded();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(movementx * speed, rb.velocity.y);
        }        
    }

    private void Jump()
    {
        if(isGrounded || jumpCount < extraJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }

    private void CheckGrounded()
    {
        if(Physics2D.OverlapCircle(groundCheck.position,0.5f, groundLayer))
        {
            isGrounded = true;
            jumpCount = 0;
            jumpCoolDown = Time.time + 0.2f;
        }
        else if(Time.time < jumpCoolDown)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator Dash(int direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravity;
    }
}
