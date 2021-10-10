using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomThirdPersonController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Animator animator;
    public float movementSpeed = 5;
    public float jumpForce = 5;
    public Transform camera;

    public Transform groundCheck;
    public LayerMask groundMask;

    public float groundDrag = 6;
    public float airDrag = 1;
    public float airMultiplier = 0.3f;


    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    public float turnSmoothTime = 0.1f;
    private float smoothVelocity;

    bool isGrounded;

    private void Start()
    {
        rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, .3f, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalMovement, 0, verticalMovement).normalized;

        if (isGrounded)
        {
            rigidbody.drag = groundDrag;
        }
        else
        {
            rigidbody.drag = airDrag;
        }


        
        
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", moveDirection.magnitude);
        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (isGrounded)
            {
                rigidbody.AddForce(moveDir.normalized * movementSpeed, ForceMode.Acceleration);
            }
            else
            {
                rigidbody.AddForce(moveDir.normalized * movementSpeed * airMultiplier, ForceMode.Acceleration);
            }
            
        }
        
    }

    void Jump()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

   
}
