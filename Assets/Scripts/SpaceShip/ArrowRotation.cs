using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{
    public GameObject target;

    public float turnSpeed;

    private void Update()
    {
        Vector3 direction = target.transform.position - this.transform.position;
        Quaternion to = Quaternion.LookRotation(direction, Vector3.up);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, to, turnSpeed * Time.deltaTime);
    }
}
