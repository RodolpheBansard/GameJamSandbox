using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MonoBehaviour
{
    public Animator animator;

    public GameObject door;
    public GameObject breakableDoor;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("HeadButt");
        }
    }

    public void DestroyDoor()
    {
        door.SetActive(false);
        breakableDoor.SetActive(true);
    }
}
