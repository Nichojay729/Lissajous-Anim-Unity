using UnityEngine;

public class LissajousAnim : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    // Parameters for Object A (with sliders)
    [Range(0.1f, 5f)] public float A_A = 1f; // Amplitude for x-axis
    [Range(0.1f, 5f)] public float B_A = 1f; // Amplitude for y-axis
    [Range(0.1f, 10f)] public float a_A = 3f; // Frequency for x-axis
    [Range(0.1f, 10f)] public float b_A = 4f; // Frequency for y-axis
    [Range(0f, 360f)] public float delta_A_degrees = 30f; // Phase shift in degrees

    // Parameters for Object B (with sliders)
    [Range(0.1f, 5f)] public float A_B = 1.5f; // Amplitude for x-axis
    [Range(0.1f, 5f)] public float B_B = 1.5f; // Amplitude for y-axis
    [Range(0.1f, 10f)] public float a_B = 2f; // Frequency for x-axis
    [Range(0.1f, 10f)] public float b_B = 3f; // Frequency for y-axis
    [Range(0f, 360f)] public float delta_B_degrees = 90f; // Phase shift in degrees

    private float time;

    void Update()
    {
        time += Time.deltaTime;

        // Convert degrees to radians
        float delta_A = delta_A_degrees * Mathf.Deg2Rad;
        float delta_B = delta_B_degrees * Mathf.Deg2Rad;

        // Calculate positions for Object A using Lissajous formula (XY plane only)
        float xA = A_A * Mathf.Sin(a_A * time + delta_A);
        float yA = B_A * Mathf.Sin(b_A * time);
        objectA.position = new Vector3(xA, yA, 0); 

        // Calculate positions for Object B using Lissajous formula (XY plane only)
        float xB = A_B * Mathf.Sin(a_B * time + delta_B);
        float yB = B_B * Mathf.Sin(b_B * time);
        objectB.position = new Vector3(xB, yB, 0);
    }
}



