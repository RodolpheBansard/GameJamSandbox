using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGel : MonoBehaviour
{
    public GameObject bluGelPrefab;

    private void OnCollisionEnter(Collision other) {
        Vector3 collisionPoint = other.contacts[0].point;

        GameObject bluGel = Instantiate(bluGelPrefab,collisionPoint,Quaternion.identity) as GameObject;
        bluGel.transform.parent = null;

        Destroy(gameObject);

    }
}
