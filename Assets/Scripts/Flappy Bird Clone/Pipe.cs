using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float step = 1;

    private void Update() {
        transform.position = new Vector3(transform.position.x-step,transform.position.y,transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<FlappyBird>()){
            other.GetComponent<FlappyBird>().playerDied();
        }
    }
}
