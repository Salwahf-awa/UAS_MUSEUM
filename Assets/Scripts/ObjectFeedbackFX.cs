using UnityEngine;

public class ObjectFeedbackFX : MonoBehaviour
{
    [Header("Spotlight (Feedback Visual)")]
    public Light spotlight;
    public float spotlightFadeSpeed = 3f;
    public float spotlightMaxIntensity = 8f;
    private float targetSpotIntensity = 0f;

    [Header("Highlight Display (Glow Objek)")]
    public Renderer highlightRenderer;
    public Color highlightColor = Color.yellow;
    private Material highlightMat;

    void Start()
    {
        if (spotlight != null)
            spotlight.intensity = 0f;

        if (highlightRenderer != null)
        {
            highlightMat = highlightRenderer.material;
            highlightMat.EnableKeyword("_EMISSION");
            highlightMat.SetColor("_EmissionColor", Color.black);
        }
    }

    void Update()
    {
        if (spotlight != null)
        {
            spotlight.intensity = Mathf.Lerp(spotlight.intensity, targetSpotIntensity, Time.deltaTime * spotlightFadeSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetSpotIntensity = spotlightMaxIntensity;
            if (highlightMat != null)
                highlightMat.SetColor("_EmissionColor", highlightColor);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetSpotIntensity = 0f;
            if (highlightMat != null)
                highlightMat.SetColor("_EmissionColor", Color.black);
        }
    }
}