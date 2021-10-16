using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiDoor : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<ThirdPersonMovement>() != null){
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<ThirdPersonMovement>() != null){
            animator.SetTrigger("Close");
        }
    }
}
