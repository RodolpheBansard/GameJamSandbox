using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        print("trigger");
        if(other.GetComponent<FlappyBird>()){
            other.GetComponent<FlappyBird>().ObstaclePassed();
        }
    }

    public float step = 1;

    private void Update() {
        transform.position = new Vector3(transform.position.x-step,transform.position.y,transform.position.z);
    }

}
