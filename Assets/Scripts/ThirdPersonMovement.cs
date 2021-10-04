using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public Transform cam;

    public Transform playerHand;

    private bool isPickingUp = false;

    public float moveSpeed = 6f;

    private Vector3 direction;
    public float turnSmoothTime = 0.1f;
    private float smoothVelocity;

    private void Update()
    {
        float horinzontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (!isPickingUp)        
            direction = new Vector3(horinzontal, 0, vertical).normalized;        
        else
            direction = new Vector3(0, 0, 0).normalized;
        
        animator.SetFloat("Speed", direction.magnitude);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Pick");
        }
    }

    public void StartPickingUpItem()
    {
        isPickingUp = true;
    }

    public void EndPickingUpItem()
    {
        isPickingUp = false;
    }

    public void PickItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerHand.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<PickupItem>())
            {
                hitCollider.GetComponent<PickupItem>().SetPickupTransform();
            }
        }

    }
}
