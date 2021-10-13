using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform targetRotation;
    public float rotationSpeed = 5;

    private void Update()
    {
        transform.RotateAround(targetRotation.position, Vector3.up, Time.deltaTime * rotationSpeed);
    }
}
