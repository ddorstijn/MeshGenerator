using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceMod : MonoBehaviour {

    [SerializeField, Range(3,120)]
    int resolution;

    [SerializeField]
    bool realtimeUpdate;

    public void GenerateCircle()
    {
        GetComponent<MeshFilter>().sharedMesh.Clear();

        Vector3[] verts = new Vector3[resolution + 1];
        int[] tris = new int[resolution * 3 + 3];
        Vector2[] uv = new Vector2[verts.Length];

        verts[verts.Length - 1] = Vector3.zero;
        uv[uv.Length - 1] = Vector2.zero;

        for (int i = 0; i < resolution; i++)
        {
            verts[i] = new Vector3(Mathf.Sin(2 * Mathf.PI / resolution * i), Mathf.Cos(2 * Mathf.PI / resolution * i), 0);
            uv[i] = verts[i];
            tris[i * 3] = i;
            tris[i * 3 + 1] = i + 1;
            tris[i * 3 + 2] = verts.Length - 1;
        }

        tris[tris.Length - 3] = resolution - 1;
        tris[tris.Length - 2] = 0;
        tris[tris.Length - 1] = resolution;

        GetComponent<MeshFilter>().sharedMesh.vertices = verts;
        GetComponent<MeshFilter>().sharedMesh.triangles = tris;

        GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
    }

    private void OnValidate()
    {
        if (realtimeUpdate)
            GenerateCircle();
    }
}
