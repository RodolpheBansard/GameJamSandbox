using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float turnSpeed = 20;
    public GameObject turretAxe;
    public GameObject cannon1;
    public GameObject cannon2;
    GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
    }

    private void Update()
    {


        Vector3 direction = new Vector3(target.transform.position.x,turretAxe.transform.position.y, target.transform.position.z) - turretAxe.transform.position;
        Quaternion to = Quaternion.LookRotation(direction, Vector3.up);
        turretAxe.transform.rotation = Quaternion.RotateTowards(turretAxe.transform.rotation, to, turnSpeed * Time.deltaTime);

        

        if (turretAxe.transform.rotation == to)
        {
            direction = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z) - cannon1.transform.position;
            to = Quaternion.LookRotation(direction, Vector3.up);
            cannon1.transform.rotation = Quaternion.RotateTowards(cannon1.transform.rotation, to, turnSpeed * Time.deltaTime);
            cannon2.transform.rotation = Quaternion.RotateTowards(cannon2.transform.rotation, to, turnSpeed * Time.deltaTime);
            
        }


    }
}
