using UnityEngine;
using System.Collections;

public class DoorAutoOpen : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        closedRot = transform.localRotation;
        openRot = Quaternion.Euler(0f, openAngle, 0f) * closedRot;
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            StopAllCoroutines();
            StartCoroutine(RotateDoor());
        }
    }

    IEnumerator RotateDoor()
    {
        float t = 0f;
        Quaternion startRot = transform.localRotation;
        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.localRotation = Quaternion.Slerp(startRot, openRot, t);
            yield return null;
        }
        transform.localRotation = openRot;
    }
}