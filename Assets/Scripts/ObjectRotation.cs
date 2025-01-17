using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public float angularSpeed = 100f;

    void Start()
    {
        // Set initial positions for Object A and Object B
        objectA.transform.position = new Vector3(0, 0, 0); // Origin
        objectB.transform.position = new Vector3(5, 0, 0); // Offset to the right

        // Make Object A bigger for better visibility
        objectA.transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        Vector3 direction = (objectB.position - objectA.position);

        // Avoid zero vector issues
        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            objectA.rotation = Quaternion.RotateTowards(
                objectA.rotation,
                targetRotation,
                angularSpeed * Time.deltaTime
            );
        }
        else
        {
            Debug.LogWarning("Object A and Object B are at the same position! Cannot rotate.");
        }
    }
}



