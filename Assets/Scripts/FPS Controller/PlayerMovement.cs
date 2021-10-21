using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;
    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 1f;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.4f;
    public float blueGelMultiplier = 2f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    bool isGrounded;
    bool isInBlueGel;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    float horizontalMovement;
    float verticalMovement;

    Rigidbody rb;
    RaycastHit slopeHit;
    float playerHeigth = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        var prevIsGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        isInBlueGel = IsInBlueGel();

        if(isGrounded && prevIsGrounded == false && isInBlueGel){
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce * blueGelMultiplier, ForceMode.Impulse);
        }

        

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down, out slopeHit,playerHeigth / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if(!isInBlueGel){
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else{
            rb.AddForce(transform.up * jumpForce * blueGelMultiplier, ForceMode.Impulse);
        }
        
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }

    private bool IsInBlueGel(){
        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, .5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag.Equals("blueGel"))
            {
                return true;
            }
        } 
        return false;
    }
    
}
