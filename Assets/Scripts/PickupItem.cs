using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 startRotation;

    public Transform playerHand;

    public void SetPickupTransform()
    {
        transform.SetParent(playerHand);

        gameObject.transform.localPosition = startPosition;
        gameObject.transform.localEulerAngles = startRotation;

    }
}
