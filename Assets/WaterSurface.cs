using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    [SerializeField]
    int size;

    [SerializeField]
    float oscillatorStrength;

    [SerializeField]
    int oscillatorSpeed;

    int step;

    Mesh mesh;


	// Use this for initialization
	void Start ()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.name = "Surface Fluid";

        // Find origin
        // Make force (origin, time)
        // For every vertex, if distance between origin and vertex is equal
        // to time, then vertex.y += 1/time;

    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        int sizePower = size*size;

        Vector3[] verts = new Vector3[(size + 1) * (size + 1)];
        int[] tris = new int[sizePower * 6];
        Vector2[] uv = new Vector2[verts.Length];

        for (int i = 0; i < verts.Length; i++)
        {
            float yOffset = Mathf.Cos(2 * Mathf.PI / oscillatorSpeed * step + i) * oscillatorStrength;
            float xOffset = Mathf.Sin(2 * Mathf.PI / oscillatorSpeed * step + i) * oscillatorStrength;

            verts[i] = new Vector3(i % (size + 1) + xOffset, 0 + yOffset, i / (size + 1));
            uv[i] = new Vector2((float)i % (float)(size+1) / (float)size, (float)i / (float)(size+1) / (float)size);

            if (i < sizePower)
            {
                // Every quad is 6 points, first 3 points are the lower triangle
                // second 3 points are bottom.

                int indicesOffset = i * 6;
                int vertexOffset = i + i / size;

                // Create lower triangle
                tris[indicesOffset + 0] = vertexOffset;
                tris[indicesOffset + 1] = vertexOffset + size + 1;
                tris[indicesOffset + 2] = vertexOffset + 1;

                // Create upper triangle
                tris[indicesOffset + 3] = vertexOffset + size + 1;
                tris[indicesOffset + 4] = vertexOffset + size + 2;
                tris[indicesOffset + 5] = vertexOffset + 1;
            }
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uv;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        if (GetComponent<BoxCollider>() == null) {
            gameObject.AddComponent<BoxCollider>();
        }

        step++;
	}
}
