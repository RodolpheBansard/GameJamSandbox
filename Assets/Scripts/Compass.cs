using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform compassHand;
    public Transform compassHandPivot;
    public Transform orientation;

    public Transform target;

    GameObject[] quests;

    private GameObject closestQuest;

    private void Start()
    {
        quests = GameObject.FindGameObjectsWithTag("Quest");
    }

    private void Update()
    {
        compassHand.LookAt(target.position);
        compassHand.localEulerAngles = new Vector3(0, compassHand.localEulerAngles.y-90, 0);
    }
}
