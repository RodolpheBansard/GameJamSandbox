using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float spinSpeed = 5;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime,0));
    }
}
