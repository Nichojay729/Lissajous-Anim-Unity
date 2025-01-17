using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public Renderer objectARenderer;

    void Update()
    {
        Vector3 forward = objectA.forward;
        Vector3 toOther = (objectB.position - objectA.position).normalized;

        float angle = Vector3.Angle(forward, toOther);
        float t = Mathf.InverseLerp(0, 180, angle);

        objectARenderer.material.color = Color.Lerp(Color.red, Color.blue, t);
    }
}



