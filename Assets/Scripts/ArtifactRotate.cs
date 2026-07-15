using UnityEngine;

public class ArtifactRotate : MonoBehaviour
{
    public float rotateSpeed = 20f;
    public Vector3 rotateAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotateAxis * rotateSpeed * Time.deltaTime);
    }
}