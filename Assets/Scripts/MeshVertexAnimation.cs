using System.Collections;
using UnityEngine;

public class MeshVertexAnimation : MonoBehaviour
{
    public float noiseScale = 0.1f; // Controls the level of Perlin noise detail
    public float speed = 1f;       // Controls the speed of animation
    private Mesh mesh;             // Reference to the mesh
    private Vector3[] originalVertices; // Stores the original vertex positions
    private Vector3[] modifiedVertices; // Stores the modified vertex positions
    private bool isMeshReady = false;   // Tracks if the mesh is ready for animation

    void Start()
    {
        // Check if the GameObject has a MeshFilter and a Mesh
        if (GetComponent<MeshFilter>() == null)
        {
            Debug.LogError("MeshFilter is missing!");
            enabled = false;
            return;
        }

        if (GetComponent<MeshFilter>().mesh == null)
        {
            Debug.LogWarning("No mesh found on MeshFilter, assigning a default mesh.");
            AssignDefaultMesh(); // Assign a default mesh if none is found
        }

        Debug.Log($"MeshVertexAnimation: Found mesh with vertex count: {GetComponent<MeshFilter>().mesh.vertexCount}");
        InitializeMesh(); // Initialize immediately if mesh is already available
    }

    IEnumerator WaitForMesh()
    {
        Debug.Log("MeshVertexAnimation: Waiting for mesh...");
        int retries = 100; // Retry up to 100 frames
        while (GetComponent<MeshFilter>().mesh == null && retries > 0)
        {
            retries--;
            Debug.Log($"MeshVertexAnimation: Retry {100 - retries}, mesh is still null...");
            yield return null; // Wait until the mesh is assigned
        }

        if (GetComponent<MeshFilter>().mesh == null)
        {
            Debug.LogError("MeshVertexAnimation: Failed to find a mesh after waiting.");
            enabled = false; // Disable the script
            yield break;
        }

        Debug.Log($"MeshVertexAnimation: Mesh found with vertex count: {GetComponent<MeshFilter>().mesh.vertexCount}");
        InitializeMesh();
    }

    void AssignDefaultMesh()
    {
        Mesh defaultMesh = new Mesh();
        defaultMesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0)
        };
        defaultMesh.triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3
        };
        defaultMesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = defaultMesh;
        Debug.Log("Default mesh assigned successfully.");
    }

    void InitializeMesh()
    {
        // Retrieve the mesh from the MeshFilter
        mesh = GetComponent<MeshFilter>().mesh;

        // Ensure the mesh is assigned
        if (mesh == null)
        {
            Debug.LogError("MeshVertexAnimation: Mesh is null! Something went wrong.");
            enabled = false; // Disable the script to prevent further errors
            return;
        }

        // Mark the mesh as dynamic
        mesh.MarkDynamic();

        // Store the original vertex positions
        originalVertices = mesh.vertices;

        if (originalVertices.Length == 0)
        {
            Debug.LogError("MeshVertexAnimation: The mesh has no vertices!");
            enabled = false;
            return;
        }

        modifiedVertices = new Vector3[originalVertices.Length];
        Debug.Log($"MeshVertexAnimation: Script initialized successfully. Vertex count: {originalVertices.Length}");
        isMeshReady = true; // Mark mesh as ready for animation
    }

    void Update()
    {
        // Check if the mesh is ready
        if (!isMeshReady)
        {
            Debug.LogWarning("MeshVertexAnimation: The mesh is not ready yet!");
            return;
        }

        // Animate the vertices
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];

            // Apply Perlin noise-based animation
            float noise = Mathf.PerlinNoise(vertex.x * noiseScale, Time.time * speed);
            modifiedVertices[i] = vertex + vertex.normalized * noise;
        }

        // Apply the modified vertices to the mesh
        mesh.vertices = modifiedVertices;

        // Recalculate normals for proper shading
        mesh.RecalculateNormals();
    }
}



