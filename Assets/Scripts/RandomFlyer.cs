using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]


public class RandomFlyer : MonoBehaviour
{
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, idleRatio;
    [SerializeField] Vector2 animSpeedMinMax, moveSpeedMinMax, changeAnimEveryFromTo;
    [SerializeField] Vector2 changeTargetEveryFromTo;
    [SerializeField] Transform homeTarget, flyingTarget;
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    [SerializeField] public bool returnToBase = false;
    [SerializeField] public float randBaseOffset = 5, delayStart = 0f;

    private Animator animator;
    private Rigidbody rb;
    [System.NonSerialized] public float changeTarget = 0f, changeAnim = 0f, timeSinceTarget = 0f, timeSinceAnim = 0f, prevAnim, currentAnim = 0f, prevSpeed, speed, zturn,prevz, turnSpeedBackup;
    private Vector3 rotateTarget, position, direction, velcity, randomizedBase;
    private Quaternion lookRotation;
    [System.NonSerialized] public float distanceFromBase, distanceFromTarget;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        turnSpeedBackup = turnSpeed;
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);
        if(delayStart < 0f)
        {
            rb.velocity = idleSpeed * direction;
        }
    }

    private void FixedUpdate()
    {
        if (delayStart > 0f)
        {
            delayStart -= Time.fixedDeltaTime;
            return;
        }

        //calculate distance
        distanceFromBase = Vector3.Magnitude(randomizedBase - rb.position);
        distanceFromTarget = Vector3.Magnitude(flyingTarget.position - rb.position);

        // Allow drastic turns close to base to ensure target can be reached
        if (returnToBase && distanceFromBase < 10f)
        {
            if (turnSpeed != 300f && rb.velocity.magnitude != 0f)
            {
                turnSpeedBackup = turnSpeed;
                turnSpeed = 300f;
            }
            else if (distanceFromBase <= 2f)
            {
                rb.velocity = Vector3.zero;
                turnSpeed = turnSpeedBackup;
                return;
            }
        }

        // Time for a new animation speed
        if (changeAnim < 0f)
        {
            prevAnim = currentAnim;
            currentAnim = ChangeAnim(currentAnim);
            changeAnim = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
            timeSinceAnim = 0f;
            prevSpeed = speed;
            if (currentAnim == 0)
            {
                speed = idleSpeed;
            }
            else
            {
                speed = Mathf.Lerp(moveSpeedMinMax.x, moveSpeedMinMax.y, (currentAnim - animSpeedMinMax.x) / (animSpeedMinMax.y - animSpeedMinMax.x));
            }
        }

        // Time for a new target position
        if (changeTarget < 0f)
        {
            rotateTarget = ChangeDirection(rb.transform.position);
            if (returnToBase)
            {
                changeTarget = 0.2f;
            }
            else
            {
                changeTarget = Random.Range(changeTargetEveryFromTo.x, changeTargetEveryFromTo.y);
            }
            timeSinceTarget = 0f;
        }

        // height
        if (rb.transform.position.y < yMinMax.x + 10f ||
            rb.transform.position.y > yMinMax.y - 10f)
        {
            if (rb.transform.position.y < yMinMax.x + 10f) rotateTarget.y = 1f; else rotateTarget.y = -1f;
        }

        zturn = Mathf.Clamp(Vector3.SignedAngle(rotateTarget, direction, Vector3.up), -45f, 45f);

        //update times
        changeAnim -= Time.fixedDeltaTime;
        changeTarget -= Time.fixedDeltaTime;
        timeSinceTarget -= Time.fixedDeltaTime;
        timeSinceAnim -= Time.fixedDeltaTime;

        // Rotate towards target
        if (rotateTarget != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
        }
        Vector3 rotation = Quaternion.RotateTowards(rb.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        rb.transform.eulerAngles = rotation;

        float temp = prevz;
        if (prevz < zturn)
        {
            prevz += Mathf.Min(turnSpeed * Time.fixedDeltaTime, zturn - prevz);
        }
        else
        {
            prevz -= Mathf.Min(turnSpeed * Time.fixedDeltaTime, prevz - zturn);
        }
        prevz = Mathf.Clamp(prevz, -45f, 45f);

        rb.transform.Rotate(0f, 0f, prevz - temp, Space.Self);

        // Move flyer
        direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;


        if (returnToBase && distanceFromBase < idleSpeed)
        {
            rb.velocity = Mathf.Min(idleSpeed, distanceFromBase) * direction;
        }
        else
        {
            rb.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(timeSinceAnim / switchSeconds, 0f, 1f)) * direction;
        }

        if (rb.transform.position.y < yMinMax.x || rb.transform.position.y > yMinMax.y)
        {
            position = rb.transform.position;
            position.y = Mathf.Clamp(position.y, yMinMax.x, yMinMax.y);
            rb.transform.position = position;
        }

    }
    private float ChangeAnim(float currentAnim)
    {
        float newState;
        if(Random.Range(0f,1f) < idleRatio)
        {
            newState = 0f;
        }
        else
        {
            newState = Random.Range(animSpeedMinMax.x, animSpeedMinMax.y);
        }

        if(newState != currentAnim)
        {
            animator.SetFloat("flySpeed", newState);
            if(newState == 0)
            {
                animator.speed = 1f;
            }
            else
            {
                animator.speed = newState;
            }
        }
        return newState;
    }

    private Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;

        if (returnToBase)
        {
            newDir = homeTarget.position - currentPosition;
        }
        else if(distanceFromTarget > radiusMinMax.y)
        {
            newDir = flyingTarget.position - currentPosition;
        }
        else if (distanceFromTarget < radiusMinMax.x)
        {
            newDir = currentPosition - flyingTarget.position;
        }
        else
        {
            float angleXZ = Random.Range(-Mathf.PI, Mathf.PI);
            float angleY = Random.Range(-Mathf.PI / 48, Mathf.PI / 48);

            newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;
        }

        

        return newDir.normalized;
    }
}
