using UnityEngine;

public class WaterSurface : MonoBehaviour
{

    [SerializeField]
    int size;

    [SerializeField]
    float oscillatorSpeed;

    [SerializeField]
    int resolution;

    int step;


	// Use this for initialization
	void Start ()
    {

    }

	// Update is called once per frame
	void Update ()
    {

        int sizePower = size*size;

        Vector3[] verts = new Vector3[sizePower];
        int[] tris = {3,1,0, 4,2,1, 6,4,3, 7,5,4,  3,4,1, 4,5,2, 6,7,4, 7,8,5};// new int[verts.Length * 3];

        float y = Mathf.Cos(2 * Mathf.PI / resolution * step);

        for (int i = 0; i < verts.Length; i++) {
            float x = Mathf.Sin(2 * Mathf.PI / resolution * step + i);
            verts[i] = new Vector3(i % size /*+ x*/, 0 /*+ y*/, i / size);

            if (i % (size-1) == 0) {
                tris[i*3] = i;
                tris[i*3+1] = i + 1;
                tris[i*3+2] = i + size;
            }
        }

        GetComponent<MeshFilter>().mesh.Clear();
        GetComponent<MeshFilter>().mesh.vertices = verts;
        GetComponent<MeshFilter>().mesh.triangles = tris;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
        step++;
	}
}
