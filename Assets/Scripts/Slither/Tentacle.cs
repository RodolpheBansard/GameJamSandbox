using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRenderer;
    public Vector3[] segmentsPoses;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    private void Start() {
        lineRenderer.positionCount = length;
        segmentsPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void Update() {
        segmentsPoses[0] = targetDir.position;

        for (var i = 1; i < segmentsPoses.Length; i++)
        {
            segmentsPoses[i] = Vector3.SmoothDamp(segmentsPoses[i], segmentsPoses[i-1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed);
        }
        lineRenderer.SetPositions(segmentsPoses);
    }
}
