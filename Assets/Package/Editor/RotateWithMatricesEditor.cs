using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotateWithMatrices))]
[CanEditMultipleObjects]
public class RotateWithMatricesEditor : Editor
{
    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RotateWithMatrices myScript = (RotateWithMatrices)target;
        if (GUILayout.Button("Rotate X"))
        {
            myScript.RotateAroundX();
        }
        if (GUILayout.Button("Rotate Y"))
        {
            myScript.RotateAroundY();
        }
        if (GUILayout.Button("Rotate Z"))
        {
            myScript.RotateAroundZ();
        }
        if (GUILayout.Button("Rotate X with Q"))
        {
            myScript.RotateAroundXQ();
        }
        if (GUILayout.Button("Rotate Y with Q"))
        {
            myScript.RotateAroundYQ();
        }
        if (GUILayout.Button("Rotate Z with Q"))
        {
            myScript.RotateAroundZQ();
        }
        if (GUILayout.Button("Reset"))
        {
            myScript.Reset();
        }
    }
}
