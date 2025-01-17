using UnityEngine;

public class ProceduralMeshCreator : MonoBehaviour
{
    public GameObject objectA; 
    public Material material; 

    void Start()
    {
        if (objectA == null)
        {
            Debug.LogError("Object A is not assigned! Please assign it in the Inspector.");
            return;
        }

        // Ensure Object A has the required components
        if (objectA.GetComponent<MeshFilter>() == null)
            objectA.AddComponent<MeshFilter>();
        if (objectA.GetComponent<MeshRenderer>() == null)
            objectA.AddComponent<MeshRenderer>();

        // Combine sphere and cone meshes
        Mesh mesh = GenerateCombinedMesh();
        objectA.GetComponent<MeshFilter>().mesh = mesh;
        Debug.Log("ProceduralMeshCreator: Mesh assigned to MeshFilter successfully.");


        // Assign material and adjust scale
        objectA.GetComponent<MeshRenderer>().material = material;
        objectA.transform.localScale = new Vector3(1, 1, 1);
    }

    Mesh GenerateCombinedMesh()
    {
        Mesh mesh = new Mesh();

        // Create sphere and cone meshes
        Mesh sphereMesh = GenerateSphereMesh(0.5f, 12, 6);
        Mesh coneMesh = GenerateConeMesh(0.2f, 0.5f, 12);

        // Combine sphere and cone vertices
        Vector3[] combinedVertices = new Vector3[sphereMesh.vertexCount + coneMesh.vertexCount];
        Vector2[] combinedUVs = new Vector2[sphereMesh.vertexCount + coneMesh.vertexCount];
        int[] combinedTriangles = new int[sphereMesh.triangles.Length + coneMesh.triangles.Length];

        // Copy sphere vertices, UVs, and triangles
        sphereMesh.vertices.CopyTo(combinedVertices, 0);
        sphereMesh.uv.CopyTo(combinedUVs, 0);
        sphereMesh.triangles.CopyTo(combinedTriangles, 0);

        // Copy cone vertices, adjust their position, and UVs
        for (int i = 0; i < coneMesh.vertexCount; i++)
        {
            combinedVertices[sphereMesh.vertexCount + i] = coneMesh.vertices[i] + new Vector3(0, 0.5f, 0); // Offset the cone
            combinedUVs[sphereMesh.vertexCount + i] = coneMesh.uv[i]; // Copy UVs
        }

        // Copy cone triangles and adjust indices
        for (int i = 0; i < coneMesh.triangles.Length; i++)
        {
            combinedTriangles[sphereMesh.triangles.Length + i] = coneMesh.triangles[i] + sphereMesh.vertexCount;
        }

        // Assign combined data to the mesh
        mesh.vertices = combinedVertices;
        mesh.triangles = combinedTriangles;
        mesh.uv = combinedUVs;

        // Recalculate normals
        mesh.RecalculateNormals();
        Debug.Log($"ProceduralMeshCreator: Generated mesh with {mesh.vertexCount} vertices.");

        return mesh;
    }

    Mesh GenerateSphereMesh(float radius, int segments, int rings)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(segments + 1) * (rings + 1)];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[segments * rings * 6];

        // Generate vertices and UVs
        for (int lat = 0; lat <= rings; lat++)
        {
            float theta = lat * Mathf.PI / rings;
            for (int lon = 0; lon <= segments; lon++)
            {
                float phi = lon * 2 * Mathf.PI / segments;
                int index = lat * (segments + 1) + lon;

                vertices[index] = new Vector3(
                    radius * Mathf.Sin(theta) * Mathf.Cos(phi),
                    radius * Mathf.Cos(theta),
                    radius * Mathf.Sin(theta) * Mathf.Sin(phi)
                );

                uvs[index] = new Vector2((float)lon / segments, (float)lat / rings); // Map UVs
            }
        }

        // Generate triangles
        int triIndex = 0;
        for (int lat = 0; lat < rings; lat++)
        {
            for (int lon = 0; lon < segments; lon++)
            {
                int current = lat * (segments + 1) + lon;
                int next = current + segments + 1;

                triangles[triIndex++] = current;
                triangles[triIndex++] = next;
                triangles[triIndex++] = current + 1;

                triangles[triIndex++] = current + 1;
                triangles[triIndex++] = next;
                triangles[triIndex++] = next + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs; // Assign UVs
        mesh.RecalculateNormals();
        return mesh;
    }

    Mesh GenerateConeMesh(float radius, float height, int segments)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[segments + 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[segments * 3 + segments * 3];

        // Generate vertices and UVs
        vertices[0] = Vector3.zero; // Base center
        uvs[0] = new Vector2(0.5f, 0.5f); // Center UV
        for (int i = 0; i < segments; i++)
        {
            float angle = i * Mathf.PI * 2 / segments;
            vertices[i + 1] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            uvs[i + 1] = new Vector2((Mathf.Cos(angle) + 1) * 0.5f, (Mathf.Sin(angle) + 1) * 0.5f);
        }
        vertices[segments + 1] = new Vector3(0, height, 0); // Cone tip
        uvs[segments + 1] = new Vector2(0.5f, 1); // Tip UV

        // Generate triangles for base
        int triIndex = 0;
        for (int i = 0; i < segments; i++)
        {
            triangles[triIndex++] = 0;
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = (i + 1) % segments + 1;
        }

        // Generate triangles for sides
        for (int i = 0; i < segments; i++)
        {
            triangles[triIndex++] = i + 1;
            triangles[triIndex++] = segments + 1;
            triangles[triIndex++] = (i + 1) % segments + 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs; // Assign UVs
        mesh.RecalculateNormals();
        return mesh;
    }
}


