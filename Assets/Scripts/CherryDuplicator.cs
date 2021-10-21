using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryDuplicator : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerParent;

    public void DuplicatePlayer(){
        GameObject newPlayer = Instantiate(playerPrefab, transform.position+new Vector3(.5f,.25f,.5f), Quaternion.identity);
        newPlayer.transform.parent = playerParent;
    }
}
