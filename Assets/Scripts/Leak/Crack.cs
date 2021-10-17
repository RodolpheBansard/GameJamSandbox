using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public GameObject flexTape;
    public GameObject particles;
    public LeakDoor leakDoor;

    private bool isSealed = false;
    private bool isInRange = false;

    

    private void Update() {
        if(Input.GetKeyDown(KeyCode.A) && isInRange){
            StartCoroutine(Tape());
        }
    }
    
    public bool GetIsSealed(){
        return isSealed;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<ThirdPersonMovement>() != null){
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<ThirdPersonMovement>() != null){
            isInRange = false;
        }
    }

    IEnumerator Tape(){        
        FindObjectOfType<ThirdPersonMovement>().GetComponent<Animator>().SetTrigger("Tape");
        yield return new WaitForSeconds(.5f);        
        
        flexTape.SetActive(true);
        particles.SetActive(false);

        isSealed = true;
        leakDoor.UpdateLeak();        
    }
}
