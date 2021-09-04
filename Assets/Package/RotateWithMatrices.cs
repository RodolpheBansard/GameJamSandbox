using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotateWithMatrices : MonoBehaviour
{
    public float angleX = 0;
    public float angleY = 0;
    public float angleZ = 0;

    Matrix4x4 RotateX;
    Matrix4x4 RotateY;
    Matrix4x4 RotateZ;

    public void Reset()
    {
        this.transform.rotation = Quaternion.identity;
    }

    public void RotateAroundX()
    {
        float aX = Mathf.Deg2Rad * angleX;
        RotateX = new Matrix4x4
        {
            m00 = 1,
            m01 = 0,
            m02 = 0,
            m03 = 0,
            m10 = 0,
            m11 = Mathf.Cos(aX),
            m12 = -Mathf.Sin(aX),
            m13 = 0,
            m20 = 0,
            m21 = Mathf.Sin(aX),
            m22 = Mathf.Cos(aX),
            m23 = 0,
            m30 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1
        };
        this.transform.forward = RotateX.MultiplyVector(this.transform.forward);
    }

    public void RotateAroundY()
    {

    }

    public void RotateAroundZ()
    {

    }

    public void RotateAroundXQ()
    {
        this.transform.Rotate(Vector3.right, angleX, Space.World);
    }
    public void RotateAroundYQ()
    {
        this.transform.Rotate(Vector3.up, angleY, Space.World);
    }
    public void RotateAroundZQ()
    {
        this.transform.Rotate(Vector3.forward, angleZ, Space.World);
    }
}
