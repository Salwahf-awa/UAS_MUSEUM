using UnityEngine;

public class RoomLightingChange : MonoBehaviour
{
    public Light roomLight;
    public Color normalColor = Color.white;
    public Color activeColor = new Color(1f, 0.85f, 0.6f);
    public float lerpSpeed = 2f;
    private Color targetColor;

    void Start()
    {
        if (roomLight != null) roomLight.color = normalColor;
        targetColor = normalColor;
    }

    void Update()
    {
        if (roomLight != null)
            roomLight.color = Color.Lerp(roomLight.color, targetColor, Time.deltaTime * lerpSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) targetColor = activeColor;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) targetColor = normalColor;
    }
}