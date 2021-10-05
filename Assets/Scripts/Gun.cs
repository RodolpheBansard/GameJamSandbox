using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = gunPoint.forward * bulletSpeed;
        }
    }
}
