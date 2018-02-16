using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SurfaceMod))]
public class SurfaceModEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SurfaceMod surfaceMod = (SurfaceMod)target;
        if (GUILayout.Button("Build Object"))
        {
            surfaceMod.GenerateCircle();
        }
    }
}
