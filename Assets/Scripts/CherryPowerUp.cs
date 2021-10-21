using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<CherryDuplicator>() != null){
            other.GetComponent<CherryDuplicator>().DuplicatePlayer();
            Destroy(gameObject);
        }
    }
}
