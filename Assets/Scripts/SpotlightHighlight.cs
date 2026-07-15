using UnityEngine;

public class SpotlightHighlight : MonoBehaviour
{
    [Header("Referensi Spotlight")]
    public Light spotlight; 

    [Header("Pengaturan Cahaya")]
    public float fadeSpeed = 10f;
    public float maxIntensity = 100f;
    private float targetIntensity = 0f;

    void Start()
    {
        if (spotlight != null)
        {
            spotlight.intensity = 0f;
            // Memastikan mode lampu otomatis Realtime saat game dimulai
            spotlight.lightmapBakeType = LightmapBakeType.Realtime;
        }
    }

    void Update()
    {
        if (spotlight == null) return;
        
        // Transisi perubahan cahaya halus
        spotlight.intensity = Mathf.MoveTowards(spotlight.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
    }

    // Fungsi pintar untuk mendeteksi Tag Player pada parent maupun child object
    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player") || (other.transform.root != null && other.transform.root.CompareTag("Player"));
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            targetIntensity = maxIntensity;
            Debug.Log("<color=green>[Spotlight] Player Masuk Area!</color> Lampu dinyalakan ke: " + maxIntensity, gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            targetIntensity = 0f;
            Debug.Log("<color=red>[Spotlight] Player Keluar Area!</color> Lampu dimatikan.", gameObject);
        }
    }
}