using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public List<Transform> grounds;
    public Vector3 resetPos;
    public float groundStep = 0.1f;

    

    void FixedUpdate()
    {
        foreach(Transform ground in grounds){
            if(ground.position.x-groundStep <= -resetPos.x/2){
                ground.position = resetPos;
            }
            ground.position = new Vector3(ground.position.x-groundStep,ground.position.y,ground.position.z);
        }
    }
}
