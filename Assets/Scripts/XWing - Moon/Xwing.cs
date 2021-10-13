using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xwing : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    private bool isOpen = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isOpen)
        {
            isOpen = false;
            animator.SetTrigger("Close");
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            isOpen = true;
            animator.SetTrigger("Open");
        }
    }
}
